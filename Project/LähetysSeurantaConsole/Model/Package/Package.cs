using Newtonsoft.Json;

namespace LähetysSeurantaConsole.Model.Package
{
    internal class Package
    {
        private static readonly HttpClient Client = new();
        public string ID { get; set; }
        public string Url;
        public Package(string iD)
        {
            ID = iD;
            UpdateTheParcel();
        }
        /// <summary>
        /// Updating the information of the parcel using the tracking number.
        /// Currently we will likely have to create different handling methods cases for each of the companies, depending on how standardized the URI components are...
        /// </summary>

        public async Task UpdateTheParcel()
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl(ID);

            using HttpResponseMessage response = await Client.GetAsync(Url);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Parcel>(json);

        }
        /// <summary>
        /// Here we are turning the given ID to an url
        /// </summary>
        /// <param name="id">The tracking number</param>
        /// <returns>The URL for the intended API </returns>
        /// <exception cref="ArgumentException"></exception>
        private static string TurningIDToUrl(string id)
        {
            char[] idarray = id.ToUpper().ToCharArray();

            switch (idarray.Take(2).ToString())
            {
                case ("FI"): return $"HTTPS://Posti.Fi/Seuranta/{id}";                                                
                case ("MA"): return $"HTTPS://Matkahuolto.fi/Seuranta/{id}";    // These are not ready urls

                default: throw new ArgumentException("Could not find the firm");                                     
            }

        }
        /// <summary>
        /// This is just a place holder to ensure that there are no immediate errors in the deserialize line.
        /// We will need to create probably an actual class that will be able to take the information from every company,
        /// but for it we probably will need to create different methods that we we'll be able to first just assign variables before sending them to the class / record which ever way we go.
        /// </summary>
        public record Parcel()
        {

        }
    }
}