using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTracking.Core.Models.Mapping.DTO
{
    /// <summary>
    /// Simulates the Posti API by returning JSON data based on the provided Posti ID.
    /// </summary>
    public class PostiAPISimulation
    {
        public static string SimulationFromTheJSON(string postiId)
        {

            switch (postiId[..2])
            {
                case "JJ": // "JJ" is Posti's id's beginning.
                    {
                        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        return File.ReadAllText(path + "/Model/Package/API/Posti.json");
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
            var random = new Random();
            var statuses = new[]
            {
                "Item picked up",
                "In transit",
                "Arrived at sorting center",
                "Out for delivery",
                "Delivered"
            };
            var events = new List<object>();
            int eventCount = random.Next(2, 6);

            DateTime time = DateTime.Now.AddDays(-random.Next(1, 5));

            for (int i = 0; i < eventCount; i++)
            {
                events.Add(new
                {
                    status = statuses[random.Next(statuses.Length)],
                    timestamp = time.AddHours(i * random.Next(2, 6)).ToString("yyyy-MM-dd HH:mm:ss"),
                    location = Citys[random.Next(Citys.Length)]
                });
            }
            var result = new
            {
                trackingCode = postiId,
                events = events
            };
            return JsonConvert.SerializeObject(result, Formatting.Indented);
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
