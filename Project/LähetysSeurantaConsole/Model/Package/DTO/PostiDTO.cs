using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package.DTO
{
    internal static class PostiDTO
    {
        /// <summary>
        /// All of this is a copypaste transfering from company DTO file to this one.
        /// Sort of must have after realizing how large of a job it was to translate the matkahuoltoDTO.
        /// This doesn't work yet.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Parcel ToParcel(string json)
        {
            return new Parcel
            {
             //   TrackingId = dto.TrackingCode ?? string.Empty,
             //   Company = "Posti",
             //   CurrentStatus = dto.Phase,
             //   StatusDescription = dto.PhaseDescription,
             //   EstimatedDelivery = dto.EstimatedDeliveryTime,
             //   DeliveredAt = dto.DeliveredTime,
             //   RecipientName = dto.RecipientName,
             //   ServiceName = dto.ServiceName,
             //   Events = dto.Events.Select(e => new ParcelEvent
             //   {
             //       Timestamp = e.Timestamp,
             //       Status = e.Phase,
             //       Description = e.Description,
             //       Location = e.Location
             //   }).ToList()
            };
        }

        internal sealed record Response
        {
            [JsonProperty("trackingCode")]
            public string? TrackingCode { get; init; }

            [JsonProperty("phase")]
            public string? Phase { get; init; }

            [JsonProperty("phaseDescription")]
            public string? PhaseDescription { get; init; }

            [JsonProperty("estimatedDeliveryTime")]
            public DateTimeOffset? EstimatedDeliveryTime { get; init; }

            [JsonProperty("deliveredTime")]
            public DateTimeOffset? DeliveredTime { get; init; }

            [JsonProperty("recipientName")]
            public string? RecipientName { get; init; }

            [JsonProperty("serviceName")]
            public string? ServiceName { get; init; }

            [JsonProperty("events")]
            public List<PostiEventDTO> Events { get; init; } = [];

            [JsonExtensionData]
            public IDictionary<string, JToken> ExtraData { get; init; } = new Dictionary<string, JToken>();
        }

        internal sealed record PostiEventDTO
        {
            [JsonProperty("timestamp")]
            public DateTimeOffset? Timestamp { get; init; }

            [JsonProperty("phase")]
            public string? Phase { get; init; }

            [JsonProperty("description")]
            public string? Description { get; init; }

            [JsonProperty("location")]
            public string? Location { get; init; }

            [JsonExtensionData]
            public IDictionary<string, JToken> ExtraData { get; init; } = new Dictionary<string, JToken>();
        }
    }
}
