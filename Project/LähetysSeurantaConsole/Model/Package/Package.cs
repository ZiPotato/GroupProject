using Newtonsoft.Json;
using System.Net.Sockets;
using System.Reflection;

namespace LähetysSeurantaConsole.Model.Package
{
    internal class Package : IPackage
    {
        private static readonly HttpClient Client = new();
        public string ID { get; set; }
        public string Company;
        public string Url;
        IPackage _model;
        Parcel IPackage.Parcel { get ; set ; }
        List<Parcel> IPackage.Parcels { get => new(); set; }

        /// <summary>
        /// Technically we should make it so we parse out the incorrectly formatted information out before this state
        /// but for the sake of testing and the origin I am doing it this way for the start atleast
        /// </summary>
        public Package(string iD, IPackage model)
        {
            _model = model;
            _model.Parcels = new List<Parcel>();
            ID = iD.ToUpper();    
            UpdateTheParcel();
        }

        /// <summary>
        /// Updating the information of the parcel using the tracking identifyer.
        /// </summary>
        public async Task UpdateTheParcel()
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl();
            using HttpResponseMessage response = await Client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            CompanyDTOs s = new(json, Company);
            _model.Parcel = s.Completed;
        }
        /// <summary
        /// This method turns the given tracking identifier into an url
        /// </summary>
        /// <param name="id"> The tracking identifier </param>
        /// <returns> The URL for the intended API </returns>
        /// <exception cref="ArgumentException"> Later in the development we should likely create our own exception class </exception>
        private string TurningIDToUrl()
        {
            char[] idarray = ID.ToCharArray();

            switch (idarray.Take(2).ToString())
            {
                case ("FI"):
                    Company = "Posti";
                    return $"HTTPS://Posti.Fi/Seuranta/{ID}";
                case ("MA"):
                    Company = "Matkahuolto";
                    return $"HTTPS://Matkahuolto.fi/Seuranta/{ID}";                     // These are not ready urls
                default:
                    throw new ArgumentException("Could not find the firm");
            }
        }
    }
}