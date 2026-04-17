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
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl(ID);    // We don't recreate the url again if it is a working url.
        
            using HttpResponseMessage response = await Client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Parcel>(json);         // In here we need to make the class for the parcel itself
            
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
                case ("FI"): return $"HTTPS://Posti.Fi/Seuranta/{id}";                                                // Here goes the firm url potentially with the already altered uri elements
                case ("MA"): return $"HTTPS://Matkahuolto.fi/Seuranta/{id}";                                                      
                default: throw new ArgumentException("Could not find the firm");                                     // We probably will need to create our own exception class that will inform the user precicely the way we want
            }

        }
        public record Parcel()      // A place holder, we should probably create another file where we have the parcel for the company that we need
        {                           // It is dependant on the actual information given to us by the API

        }
    }
}