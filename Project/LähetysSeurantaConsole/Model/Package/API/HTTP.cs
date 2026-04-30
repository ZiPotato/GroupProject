using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package.API
{
    internal class HTTP
    {
        
        public string Company = string.Empty;
        public async Task<string> FindAndUseTheAPI(string Url, string ID)
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID))
            {
                Url = TurningIDToUrl(ID);
            }

            string credentials = CreateAuth();
        
            using HttpRequestMessage request = new(HttpMethod.Get, Url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            HttpClient Client = new();

            using HttpResponseMessage response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }
        private string CreateAuth()
        {
            switch (Company)
            {
                case ("MH"):
                    {
                        return MHAuthentication();
                    }
                default:
                    {
                        throw new ArgumentException("How did you get this far with a wrong firm? ");
                    }
            }
        }
        /// <summary>
        /// Generates a Base64-encoded HTTP Basic Authentication header value using a predefined username and password.
        /// We need to create a local file that is referenced rather than the code, but since we don't have any keys this is low priority.
        /// 
        /// There is a simple way in my mind we could make this "work".
        /// We simply copy paste:
        /// var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        /// return File.ReadAllText(path + "/model/package/api/Keys.json");
        /// 
        /// into the code and exclude the Keys.json from the github with the gitignore. This way we may read it as much we want and use it as much as we want while it is still only kept locally. 
        /// This is quite barbaric and and simple way of it, but at the same time it should be fine.
        /// </summary>
        private static string MHAuthentication()
        {
            string? username = "";                // UlkAPIAvoin
            string? password = "";                // BUs28DefuNab?8aj3p3eqega
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }

        /// <summary>
        /// Generates a tracking URL for a shipment based on the provided tracking ID.
        /// </summary>                                     
        private string TurningIDToUrl(string ID)
        {
            DateTime now = DateTime.Now;
            DateTime anHourAgo = now.AddHours(-1);

            string id = ID[..2];
            string from = Uri.EscapeDataString(anHourAgo.ToString("yyyy-MM-ddTHH:mm:ss"));
            string to = Uri.EscapeDataString(now.ToString("yyyy-MM-ddTHH:mm:ss"));
            string trackingId = Uri.EscapeDataString(ID);

            switch (id)
            {
                case "MH":
                    Company = "MH";
                    return $"https://extservicestest.matkahuolto.fi/mpaketti/public/tracking?ids={trackingId}&from={from}&to={to}"; // Likely real.
//                case "FI":
//                    Company = "FI";
//                    return $"https://api.posti.fi/tracking/7/shipments/trackingnumber/{trackingId}"; Potentially not real.
                default:
                    switch (ID[(ID.Length - 3)..])
                    {
//                        case "SE":
//                            Company = "SE";
//                            return $"https://api2.postnord.com/rest/shipment/v5/trackandtrace/findByIdentifier.json?apikey={This portion is ridiculous way to hide your APIkey}&id={trackingId}&locale=fi"; // We will not be using this at the end portion of this                      
                        default: throw new ArgumentException("Could not find the firm");
                    }
            }
        }

    }
}
