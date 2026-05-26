using Newtonsoft.Json;

namespace OrderTracking.Core.Models.Mapping.API
{
    /// <summary>
    /// Simulates the Posti API by returning JSON data based on the provided Posti ID.
    /// </summary>
    public static class PostiAPISimulation
    {
        private static readonly Random Rand = Random.Shared;

        public static string SimulationFromTheJSON(string postiId)
        {
            return postiId[..2] switch
            {
                "JJ" => "{\"trackingCode\":\"JJFI00000000000000\",\"productName\":\"Postipaketti\",\"events\":[{\"timestamp\":\"2024-05-12T10:00:00+03:00\",\"locationName\":\"HELSINKI\",\"description\":\"Lähetys on toimitettu noutopisteeseen.\"},{\"timestamp\":\"2024-05-11T14:30:00+03:00\",\"locationName\":\"VANTAA\",\"description\":\"Lähetys on lajiteltu.\"}],\"estimatedDeliveryTime\":\"2024-05-12T16:00:00+03:00\"}",
                _ => throw new ArgumentException("Couldn't find a firm")
            };
        }

        public static string SimulatingRandomPosti(string postiId)
        {
            var descriptions = new[]
            {
                "Item picked up",
                "In transit",
                "Arrived at sorting center",
                "Out for delivery",
                "Delivered"
            };

            var events = new List<object>();
            int eventCount = Rand.Next(2, 6);
            DateTimeOffset firstEventTime = GetRandomEventTime();

            for (int i = 0; i < eventCount; i++)
            {
                events.Add(new
                {
                    timestamp = firstEventTime.AddHours(i * Rand.Next(2, 6)),
                    locationName = Cities[Rand.Next(Cities.Length)],
                    description = descriptions[Rand.Next(descriptions.Length)]
                });
            }

            var orderedEvents = events.OrderBy(e => ((dynamic)e).timestamp).ToList();

            var latestDescription = (string)((dynamic)orderedEvents.Last()).description;
            DateTimeOffset? deliveryDate = latestDescription == "Delivered" ? (DateTimeOffset)((dynamic)orderedEvents.Last()).timestamp : null;

            var result = new
            {
                trackingCode = postiId,
                productName = "Postipaketti",
                deliveryDate, // maps to PostiDTO.Response.DeliveredAt
                events = orderedEvents,
                estimatedDeliveryTime = DateTimeOffset.Now.AddHours(Rand.Next(1, 8))
            };

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        private static DateTimeOffset GetRandomEventTime()
        {
            return DateTimeOffset.Now
                .AddHours(-Rand.Next(0, 24))
                .AddMinutes(-Rand.Next(0, 60));
        }

        private static readonly string[] Cities =
        {
            "Helsinki",
            "Espoo",
            "Tampere",
            "Vantaa",
            "Oulu",
            "Turku",
            "Jyväskylä",
            "Lahti",
            "Kuopio",
            "Pori"
        };
    }
}