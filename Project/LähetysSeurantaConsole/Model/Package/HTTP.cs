using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package
{
    internal class HTTP
    {
        private static readonly HttpClient Client = new();
        public string Company = string.Empty;
        public async Task<string> FindAndUseTheAPI(string Url, string ID)
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID))
            {
                Url = TurningIDToUrl(ID);
            }

            using HttpRequestMessage request = new(HttpMethod.Get, Url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            CreateAuth(request);
            using HttpResponseMessage response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return json;
        }
        private void CreateAuth(HttpRequestMessage request)
        {
            switch (Company)
            {
                case ("MH"):
                    {
                        MHAuthentication(request);
                        break;
                    }
            }
        }
        private static void MHAuthentication(HttpRequestMessage request)
        {
            string? username = "UlkAPIAvoin";                   // These are the test credentials provided in : https://www.matkahuolto.fi/matkahuolto-open-interfaces
            string? password = "BUs28DefuNab?8aj3p3eqega";      

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Matkahuolto credentials are missing. Set MATKAHUOLTO_USERNAME and MATKAHUOLTO_PASSWORD environment variables.");
            }

            string rawCredentials = $"{username}:{password}";
            string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(rawCredentials));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
        }
        private string TurningIDToUrl(string ID)
        {
            DateTime now = DateTime.Now;
            DateTime anHourAgo = now.AddHours(-1);

            if (ID.Length < 2)
            {
                throw new ArgumentException("Tracking ID must contain at least two characters.");
            }

            string id = ID[..2];

            switch (id)
            {
                case "MH":
                    Company = "MH";

                    string from = Uri.EscapeDataString(anHourAgo.ToString("yyyy-MM-ddTHH:mm:ss"));
                    string to = Uri.EscapeDataString(now.ToString("yyyy-MM-ddTHH:mm:ss"));
                    string trackingId = Uri.EscapeDataString(ID);

                    return $"https://extservicestest.matkahuolto.fi/mpaketti/public/tracking?ids={trackingId}&from={from}&to={to}";

                default:
                    throw new ArgumentException("Could not find the firm");
            }
        }

    }
}
