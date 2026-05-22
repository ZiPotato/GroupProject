using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;
using OrderTracking.Core.Models.Package;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderTracking.Core.Models.Mapping.DTO
{
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
                .OrderByDescending(e => e.EventTime)
                .FirstOrDefault();

            DateTimeOffset? deliveredAt = events
                .Where(e => e.EventCode is "60" or "61")
                .OrderByDescending(e => e.EventTime)
                .Select(e => e.EventTime)
                .FirstOrDefault();

            string trackingId =
                latestEvent?.TrackingCode
                ?? rootTracingCode
                ?? latestEvent?.Description
                ?? string.Empty;

            return new Parcel
            {
                TrackingId = latestEvent?.ShipmentNumber
                    ?? latestEvent?.ParcelNumber
                    ?? string.Empty,
                Company = "Posti",
                StatusDescription = latestEvent?.Description,
                DeliveredAt = deliveredAt,
                Events = events
                    .OrderBy(e => e.EventTime)
                    .Select(e => new ParcelEvent
                    {
                        Timestamp = e.EventTime,
                        Location = e.LocationName,
                        Description = e.Description
                    })
                    .ToList()
            };
        }
    }
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
        [JsonProperty("ShipmentNumber")]
        public string? ShipmentNumber { get; init; }

        [JsonProperty("ParcelNumber")]
        public string? ParcelNumber { get; init; }
        
        [JsonProperty("EventCode")]
        public string? EventCode { get; init; }

        [JsonProperty("EventTime")]
        public DateTimeOffset? EventTime { get; init; }


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
    
}
