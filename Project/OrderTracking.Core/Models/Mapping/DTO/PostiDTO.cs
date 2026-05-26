using Newtonsoft.Json;
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Models.Mapping.DTO;

public static class PostiDTO
{
    public static Parcel ToParcel(string json)
    {
        var dto = JsonConvert.DeserializeObject<Response>(json)
            ?? throw new JsonSerializationException("Posti JSON could not be deserialized.");

        var orderedEvents = dto.Events?
            .Where(e => e is not null)
            .OrderBy(e => e.Timestamp)
            .ToList() ?? [];

        var latestEvent = orderedEvents.LastOrDefault();

        return new Parcel
        {
            TrackingId = dto.TrackingCode ?? string.Empty,
            Company = "Posti",
            StatusDescription = latestEvent?.Description,
            DeliveredAt = dto.DeliveredAt,
            
            ETA = dto.EstimatedDeliveryTime?.LocalDateTime,
            Events = orderedEvents
                .Select(e => new ParcelEvent
                {
                    Timestamp = e.Timestamp,
                    Description = e.Description,
                    Location = e.LocationName
                })
                .ToList()
        };
    }

    internal sealed record Response
    {
        [JsonProperty("trackingCode")]
        public string? TrackingCode { get; init; }

        [JsonProperty("productName")]
        public string? ProductName { get; init; }
        [JsonProperty("deliveryDate")]
        public DateTimeOffset? DeliveredAt { get; init; }

        [JsonProperty("events")]
        public List<PostiEvent> Events { get; init; } = [];

        [JsonProperty("estimatedDeliveryTime")]
        public DateTimeOffset? EstimatedDeliveryTime { get; init; }
    }

    internal sealed record PostiEvent
    {
        [JsonProperty("timestamp")]
        public DateTimeOffset? Timestamp { get; init; }

        [JsonProperty("locationName")]
        public string? LocationName { get; init; }

        [JsonProperty("description")]
        public string? Description { get; init; }
    }
}
