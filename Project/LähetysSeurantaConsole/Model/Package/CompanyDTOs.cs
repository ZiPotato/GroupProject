using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace LähetysSeurantaConsole.Model.Package
{
    internal class CompanyDTOs
    {
        /// <summary>
        /// Basically here will be the Company DTO:s (Data Transfer Objects) So we handle each of the Jsons
        /// 
        /// From these Jsons we will create objects that we then will turn to our parcels so they can share the same information with the same language.
        /// </summary>
        public Parcel Completed;
        public CompanyDTOs(string json, string company) 
        {
            Completed = DTOHandle(json, company);
        }
        private Parcel DTOHandle(string json, string company)
        {
            var temp = JObject.Parse(json);
            switch (company)
            {
                case ("Matkahuolto"):
                    return MatkahuoltoToParcel(temp.ToObject<MatkahuoltoDTO>());
                case ("Posti"):
                    return PostiToParcel(temp.ToObject<PostiDTO>()); 
                default: 
                    throw new ArgumentException("The desired company was not found");
            }
        }
        /// <summary>
        /// This is how we turn the MatkahuoltoDTO into a parcel.
        /// </summary>
        /// <param name="dto"> This is the DTO generated from the Json data given to us by the API </param>
        /// <returns> The completed Parcel </returns>
        private static Parcel MatkahuoltoToParcel(MatkahuoltoDTO dto)
        {
            return new Parcel
            {
                TrackingId = dto.TrackingCode ?? string.Empty,
                Company = "Matkahuolto",
                CurrentStatus = dto.Status,
                StatusDescription = dto.StatusDescription,
                EstimatedDelivery = dto.EstimatedDeliveryTime,
                DeliveredAt = dto.DeliveredTime,
                RecipientName = dto.RecipientName,
                ServiceName = dto.ServiceName,
                Events = dto.Events.Select(e => new ParcelEvent
                {
                    Timestamp = e.Timestamp,
                    Status = e.Status,
                    Description = e.Description,
                    Location = e.Location
                }).ToList()
            };
        }
        /// <summary>
        /// Will not work currently. I am still researching the format in which the Json will come.
        /// </summary>
        private static Parcel PostiToParcel(PostiDTO dto)
        {
            return new Parcel
            {
                TrackingId = dto.TrackingCode ?? string.Empty,
                Company = "Posti",
                CurrentStatus = dto.Phase,
                StatusDescription = dto.PhaseDescription,
                EstimatedDelivery = dto.EstimatedDeliveryTime,
                DeliveredAt = dto.DeliveredTime,
                RecipientName = dto.RecipientName,
                ServiceName = dto.ServiceName,
                Events = dto.Events.Select(e => new ParcelEvent
                {
                    Timestamp = e.Timestamp,
                    Status = e.Phase,
                    Description = e.Description,
                    Location = e.Location
                }).ToList()
            };
        }
        internal sealed record MatkahuoltoDTO
        {
            [JsonProperty("trackingCode")]
            public string? TrackingCode { get; init; }

            [JsonProperty("carrier")]
            public string? Carrier { get; init; }

            [JsonProperty("status")]
            public string? Status { get; init; }

            [JsonProperty("statusDescription")]
            public string? StatusDescription { get; init; }

            [JsonProperty("estimatedDeliveryTime")]
            public DateTimeOffset? EstimatedDeliveryTime { get; init; }

            [JsonProperty("deliveredTime")]
            public DateTimeOffset? DeliveredTime { get; init; }

            [JsonProperty("recipientName")]
            public string? RecipientName { get; init; }

            [JsonProperty("serviceName")]
            public string? ServiceName { get; init; }

            [JsonProperty("events")]
            public List<MatkahuoltoEventDTO> Events { get; init; } = [];

            [JsonExtensionData]
            public IDictionary<string, JToken> ExtraData { get; init; } = new Dictionary<string, JToken>();
        }

        internal sealed record MatkahuoltoEventDTO
        {
            [JsonProperty("timestamp")]
            public DateTimeOffset? Timestamp { get; init; }

            [JsonProperty("status")]
            public string? Status { get; init; }

            [JsonProperty("description")]
            public string? Description { get; init; }

            [JsonProperty("location")]
            public string? Location { get; init; }

            [JsonExtensionData]
            public IDictionary<string, JToken> ExtraData { get; init; } = new Dictionary<string, JToken>();
        }
        internal sealed record PostiDTO
        {
            [JsonProperty("trackingCode")]
            public string? TrackingCode { get; init; }

            [JsonProperty("carrier")]
            public string? Carrier { get; init; } 

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
    }
}
