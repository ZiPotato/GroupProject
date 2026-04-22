using System.Security.Principal;

namespace LähetysSeurantaConsole.Model.Package
{
    internal class PackageModeling : IPackage
    {
        private static readonly HttpClient Client = new();
        public string ID { get; set; }
        public string Company;
        public string Url;
        IPackage _model;
        Parcel IPackage.CompletedParcel { get ; set ; }

        public Parcel UpdateParcel(Parcel par)
        {
            ID = par.TrackingId;
            Url = par.URL;
            GetTheParcel();
            return _model.CompletedParcel;
        }

        public Parcel GetTheParcel(string id)
        {
            ID = id;
            GetTheParcel();
            return _model.CompletedParcel;
        }

        public async Task GetTheParcel()
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl();
            using HttpResponseMessage response = await Client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            CompanyDTO dto = new(json, Company);
            _model.CompletedParcel = dto.Completed;
            _model.CompletedParcel.URL = Url;
            Url = string.Empty;
        }

        /// <summary>
        /// This is how we handle the ID and turn it into the url we need, it currently is rough and unready, since we do not even handle the APIs.
        /// </summary>
        /// <returns> Eventually the completed url </returns>
        /// <exception cref="ArgumentException"></exception>
        private string TurningIDToUrl()
        {
            DateTime weekago = DateTime.Now.AddDays(-7);
            char[] idarray = ID.ToCharArray();
            switch (idarray.Take(2).ToString())
            {
                case ("MA"):
                    Company = "MA";
                    return $"HTTPS://extservicetest.matkahuolto.fi/mpaketti/public/tracking/?ids=<{ID}>&from={weekago}>&to=<{DateTime.Now}>";  // Currently we are using the API meant for testing (found in their own documentation)
                default:
                    throw new ArgumentException("Could not find the firm");
            }
        }
    }
}