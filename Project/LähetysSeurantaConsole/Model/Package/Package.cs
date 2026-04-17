using Newtonsoft.Json;
namespace LähetysSeurantaConsole.Model.Package
{
    internal class Package
    {
        public string ID { get; set; }
        public string Company { get; set; }
        public bool Delivered { get; set; }
        public string Url;
        public string State;
        public Package(string iD)
        {
            ID = iD;
            UpdateTheParcel();
        }
        /// <summary>
        /// Updating the information of the parcel using the tracking number
        /// </summary>
        public void UpdateTheParcel()
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl(ID);    // We don't recreate the url again if it is a working url.

                                                    // https://assets.ctfassets.net/lt6mvg8ztynj/2Vk56s36xcB3zt5LmhJVh/d1c0e550aa35d8088ddb7dae20fc958b/MHTracking_FinV1.3.pdf Potentially a good place to start
        }
    
        /// <summary>
        /// Here we are turning the given ID to an url
        /// </summary>
        /// <param name="id">The tracking number</param>
        /// <returns>The URL for the intended API</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string TurningIDToUrl(string id)   
        {
            char[] idarray = id.ToCharArray();
            
            switch (idarray.Take(2).ToString())
            {
                case ("FI"): return $"HTTPS://Posti.Fi/Seuranta{id}";                                                // Here goes the firm url potentially with the already altered uri elements
                case ("MA"): return $"HTTPS://Matkahuolto.fi/Seuranta{id}";         
                default: throw new ArgumentException("Could not find the firm");                                     // We probably will need to create our own exception class that will inform the user precicely the way we want
            }

        }
    }
}