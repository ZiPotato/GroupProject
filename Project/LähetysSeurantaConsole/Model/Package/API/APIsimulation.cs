using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LähetysSeurantaConsole.Model.Package.API
{
    public class APIsimulation
    {
        private static readonly Random Rand = Random.Shared;

        /// <summary>
        /// This is used to simulate retrieving their own test data from a given API.
        /// This doesn't actually use the ID for anything other than choosing the company.
        /// </summary>
        public static string SimulationFromTheXML(string id)
        {
            switch (id[..2])
            {
                case "MH":
                    {
                        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        return File.ReadAllText(path + "/model/package/api/MH.json");
                    }
                default:
                    throw new ArgumentException("Couldn't find a firm");
            }
        }

        public static string SimulatingRandom(string id)
        {
            string shipmentNumber = id.ToUpperInvariant();
            string status = GetTheStatus();
            string eventPlace = GetTheCity().ToUpperInvariant();
            DateTimeOffset eventTime = GetRandomEventTime();

            var data = new
            {
                Event = new
                {
                    EventId = Rand.NextInt64(100000000000, 999999999999),
                    ShipmentNumber = shipmentNumber,
                    ParcelNumber = shipmentNumber,
                    SenderReference = $"SR{Rand.Next(10000, 99999)}",
                    EventCode = int.Parse(status),
                    EventTime = eventTime,
                    EventPlace = eventPlace,
                    OfficeCode = Rand.Next(1, 100),
                    Signature = GetSignature(status),
                    Remarks = GetRemarks(status),
                    ReturnShipmentNumber = string.Empty
                }
            };

            return JsonConvert.SerializeObject(data);
        }

        private static string GetTheStatus()
        {
            string[] statuses =
            [
                "02",
                "08",
                "10",
                "12",
                "15",
                "25",
                "35",
                "40",
                "41",
                "45",
                "46",
                "47",
                "48",
                "50",
                "55",
                "56",
                "57",
                "60",
                "61",
                "62",
                "65",
                "70",
                "97",
                "104"
            ];

            return statuses[Rand.Next(statuses.Length)];
        }

        private static string GetTheCity()
        {
            List<string> cities =
            [
                "Helsinki",
                "Jyväskylä",
                "Pori",
                "Turku",
                "Oulu",
                "Siellä",
                "Täällä",
                "Saimaa",
                "Kajaani",
                "Tampere"
            ];

            return cities[Rand.Next(cities.Count)];
        }
        private static DateTimeOffset GetRandomEventTime()
        {
            return DateTimeOffset.Now
                .AddHours(-Rand.Next(0, 24))
                .AddMinutes(-Rand.Next(0, 60));
        }


        private static string GetSignature(string status)
        {
            if (status == "60" || status == "61") return "Mini Me";
            else return string.Empty;
        }

        private static string GetRemarks(string status)
        {
            return status switch
            {
                "50" => "Ready for pickup",
                "55" => "Customer notified",
                "56" => "Customer notified again",
                "60" => "Shipment delivered",
                "61" => "Delivered by authorization",
                "62" => "Delivery cancelled",
                "97" => "Delivery failed, transferred to office",
                _ => "Additional Info"
            };
        }
    }
}
