using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OrderTracking.Core.Models.Mapping.DTO
{
    /// <summary>
    /// Simulates the Posti API by returning JSON data based on the provided Posti ID.
    /// </summary>
    public class PostiAPISimulation
    {
        private static readonly Random Rand = Random.Shared;
        public static string SimulationFromTheJSON(string postiId)
        {


            switch (postiId[..2])
            {
                case "JJ": // "JJ" is Posti's id's beginning.
                    {
                        //var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        //return File.ReadAllText(path + "/Model/Package/API/Posti.json");
                        return "{\"trackingCode\":\"JJFI00000000000000\",\"productName\":\"Postipaketti\",\"events\":[{\"timestamp\":\"2024-05-12T10:00:00+03:00\",\"locationName\":\"HELSINKI\",\"description\":\"Lähetys on toimitettu noutopisteeseen.\"},{\"timestamp\":\"2024-05-11T14:30:00+03:00\",\"locationName\":\"VANTAA\",\"description\":\"Lähetys on lajiteltu.\"}],\"estimatedDeliveryTime\":\"2024-05-12T16:00:00+03:00\"}";


                    }
                default:
                    throw new ArgumentException("Couldn't find a firm");
            }
        }

        public static string SimulatingRandomPosti(string postiId)
        {
            if (!postiId.StartsWith("JJ"))
            {
                throw new ArgumentException("Invalid Posti ID");
            }
            
            var statuses = new[]
            {
                "Item picked up",
                "In transit",
                "Arrived at sorting center",
                "Out for delivery",
                "Delivered"
            };
            var events = new List<object>();

            int eventCount = Rand.Next(2, 6);

            DateTimeOffset eventTime = GetRandomEventTime();

            for (int i = 0; i < eventCount; i++)
            {
                events.Add(new
                {
                    timestamp = eventTime.AddHours(i * Rand.Next(2, 6)),
                    location = Citys[Rand.Next(Citys.Length)],
                    status = statuses[Rand.Next(statuses.Length)]
                });
            }
            var result = new
            {
                trackingCode = postiId,
                productName = "Postipaketti",
                events = events.OrderByDescending(e => ((dynamic)e).timestamp),
                estimatedDeliveryTime = DateTime.Now.AddHours(Rand.Next(1, 8))
            };
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
        private static DateTimeOffset GetRandomEventTime()
        {
            return DateTimeOffset.Now
                .AddHours(-Rand.Next(0, 24))
                .AddMinutes(-Rand.Next(0, 60));
        }

        private static readonly String[] Citys =
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
