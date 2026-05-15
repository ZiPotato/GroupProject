using System;
using System.Collections.Generic;
using System.Text;
using OrderTracking.Core.Models.Package;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderTracking.Core.Models.Mapping.DTO
{
    internal static class MatkahuoltoDTO
    {
        /// <summary>
        /// This is basically how we return the Parcel from here.
        /// </summary>
        /// <param name="json"></param>
        /// <returns> The completed and modeled parcel </returns>
        public static Parcel ToParcel(string json)
        {
            JObject root = JObject.Parse(json);

            JToken trackingEventsToken = root["MHTrackingEvents"] ?? root;

            JToken? eventToken = trackingEventsToken["Event"];
            if (eventToken is JObject singleEvent)
            {
                trackingEventsToken["Event"] = new JArray(singleEvent);
            }

            TrackingEvents trackingEvents = trackingEventsToken.ToObject<TrackingEvents>() ?? throw new JsonSerializationException("Matkahuolto JSON could not be deserialized.");

            Response dto = new()
            {
                MHTrackingEvents = trackingEvents
            };

            return DTOtoParcel(dto);
        }

        /// <summary>
        /// Here we use the gathered DTO data to parcel.
        /// </summary>
        private static Parcel DTOtoParcel(Response dto)
        {
            if (dto.MHTrackingEvents?.Error is not null)
            {
                throw new ArgumentException(
                    $"Matkahuolto returned an error: {dto.MHTrackingEvents.Error.ErrorCode} {dto.MHTrackingEvents.Error.ErrorText}");
            }

            List<Event> events = dto.MHTrackingEvents?.Events ?? [];

            Event? latestEvent = events
                .OrderByDescending(e => e.EventTime)
                .FirstOrDefault();

            DateTimeOffset? deliveredAt = events
                .Where(e => e.EventCode is "60" or "61")
                .OrderByDescending(e => e.EventTime)
                .Select(e => e.EventTime)
                .FirstOrDefault();

            return new Parcel
            {
                TrackingId = latestEvent?.ShipmentNumber
                    ?? latestEvent?.ParcelNumber
                    ?? string.Empty,
                Company = "Matkahuolto",
                StatusDescription = DecipherEvent(latestEvent?.EventCode),
                DeliveredAt = deliveredAt,
                Events = events
                    .OrderBy(e => e.EventTime)
                    .Select(e => new ParcelEvent
                    {
                        Timestamp = e.EventTime,
                        Description = EventToDescription(e),
                        Location = e.EventPlace
                    })
                    .ToList()
            };
        }

        private static string EventToDescription(Event e)
        {
            string description = DecipherEvent(e.EventCode) ?? "Unknown event";

            if (!string.IsNullOrWhiteSpace(e.Remarks))
            {
                return $"{description} ({e.Remarks})";
            }
            return description;
        }

        /// <summary>
        /// Matkahuolto API uses a numberbased system to inform us about the state of the package,
        /// So here we decipher the information using the documentation that is in the project folder.
        /// </summary>
        /// <param name="eventCode"> The number given to us by the API </param>
        /// <returns> A string that describes the state of the parcel </returns>
        public static string? DecipherEvent(string? eventCode)
        {
            return eventCode switch
            {
                "02" => "Electronic advance information received",
                "08" => "Picked up",
                "2" => "Electronic advance information received",
                "8" => "Picked up",
                "10" => "Left parcel point",
                "12" => "Consolidated",
                "15" => "Received for transport",
                "25" => "Loaded into trunk cargo",
                "35" => "Arrival shelved",
                "40" => "Waiting for loading to distribution",
                "41" => "Waiting for loading to pickup point",
                "45" => "Loaded to delivery route",
                "46" => "Loaded to pickup point",
                "47" => "Delivered to parcel point",
                "48" => "Arrived at parcel point",
                "50" => "Ready for pickup",
                "55" => "Notified (1st time)",
                "56" => "Notified (2nd time)",
                "57" => "Notified manually",
                "60" => "Delivered",
                "61" => "Delivered by authorization",
                "62" => "Delivery cancelled",
                "65" => "Bus advance paid to sender",
                "70" => "Returned unclaimed",
                "97" => "Delivery failed, transferred to office",
                "104" => "Reservation added",
                _ => $"Unknown event code: {eventCode}"
            };
        }

        internal sealed record Response
        {
            [JsonProperty("MHTrackingEvents")]
            public TrackingEvents? MHTrackingEvents { get; init; }
        }

        internal sealed record TrackingEvents
        {
            [JsonProperty("Event")]
            public List<Event> Events { get; init; } = [];

            [JsonProperty("Error")]
            public Error? Error { get; init; }
        }

        internal sealed record Event
        {
            [JsonProperty("ShipmentNumber")]
            public string? ShipmentNumber { get; init; }

            [JsonProperty("ParcelNumber")]
            public string? ParcelNumber { get; init; }

            [JsonProperty("EventCode")]
            public string? EventCode { get; init; }

            [JsonProperty("EventTime")]
            public DateTimeOffset? EventTime { get; init; }

            [JsonProperty("EventPlace")]
            public string? EventPlace { get; init; }

            [JsonProperty("Remarks")]
            public string? Remarks { get; init; }
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
}
