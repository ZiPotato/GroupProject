using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using OrderTracking.Core.Models.Package;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderTracking.Core.Models.Mapping.DTO
{
    public class PostiTrackingDto
    {
        [JsonProperty("trackingCode")]
        public string? TrackingCode { get; init; }
        
        [JsonProperty("productName")]
        public string? ProductName { get; init; }

        [JsonProperty("events")]
        public List<PostiEvent>? Events { get; init; }

        [JsonProperty("estimatedDeliveryTime")]
        public DateTime? EstimatedDeliveryTime { get; init; }
    }
    public class PostiEvent
    {
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp{ get; init; }

        [JsonProperty("locationName")]
        public string? LocationName { get; init; }

        [JsonProperty("description")]
        public string? Description { get; init; }

        [JsonProperty("trackingCode")]
        public string? TrackingCode { get; init; }
    }
    internal sealed record TrackingEvents
    {
        [JsonProperty("events")]
        public List<PostiEvent>? Events { get; init; }

        [JsonProperty("Error")]
        public Error? Error { get; init; }

    }
    internal sealed record Response
    {
        [JsonProperty("PostiTrackingEvents")]
        public TrackingEvents? PostiTrackingEvents { get; init; }
    }
    internal sealed record Error
    {
        [JsonProperty("EventId")]
        public string? EventId { get; init; }

        [JsonProperty("ErrorCode")]
        public string? ErrorCode { get; init; }

        [JsonProperty("ErrorText")]
        public string? ErrorText { get; init; }
    }
    public static class PostiDTO
    {

        public static Parcel ToParcel(string json)
        {
            JObject root = JObject.Parse(json);

            string? rootTracingCode = root["trackingCode"]?.ToString();

            JToken? trackingEventToken = root["PostiTrackingEvents"] ?? root;

            JToken? eventToken = trackingEventToken["Event"];
            if (eventToken is JObject singleEvent)
            {
                trackingEventToken["events"] = new JArray(singleEvent);
            }

            TrackingEvents trackingEvents = trackingEventToken.ToObject<TrackingEvents>() ?? throw new JsonSerializationException("Posti JSON could not be deserialized.");

            Response dto = new()
            {
                PostiTrackingEvents = trackingEvents
            };
            return DTOtoParcel(dto, rootTracingCode);
        }

        private static Parcel DTOtoParcel(Response dto, string? rootTracingCode)
        {
            if (dto.PostiTrackingEvents?.Error is not null)
            {
                throw new ArgumentException(
                    $"Posti returned an error: {dto.PostiTrackingEvents.Error.ErrorCode} - {dto.PostiTrackingEvents.Error.ErrorText}");
            }
            List<PostiEvent> events = dto.PostiTrackingEvents?.Events ?? [];

            PostiEvent? latestEvent = events
                .OrderByDescending(e => e.Timestamp)
                .FirstOrDefault();

            string trackingId = 
                latestEvent?.TrackingCode
                ?? rootTracingCode
                ?? latestEvent?.Description
                ?? string.Empty;

            return new Parcel
            {
                TrackingId = trackingId,
                Company = "Posti",
                StatusDescription = latestEvent?.Description,
                DeliveredAt = latestEvent?.Timestamp.UtcDateTime,
                Events = events
                    .OrderBy(e => e.Timestamp)
                    .Select(e => new ParcelEvent
                    {
                        Timestamp = e.Timestamp,
                        Location = e.LocationName,
                        Description = e.Description
                    })
                    .ToList()
            };
        }
    }
}
