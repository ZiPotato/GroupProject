using System.Security.Principal;

namespace LähetysSeurantaConsole.Model.Package
{
    public class PackageModeling : IPackage
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
            FindAndUseTheAPI();
            return _model.CompletedParcel;
        }

        public Parcel GetTheParcel(string id)
        {
            ID = id;
            FindAndUseTheAPI();
            return _model.CompletedParcel;
        }

        public async Task FindAndUseTheAPI()
        {
            if (string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(ID)) Url = TurningIDToUrl();
            using HttpResponseMessage response = await Client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            JsonToParcel(json);
        }
        /// <summary>
        /// Decided to extract this to be its own method for the sake of testing
        /// and I guess it does make it easier to read especiallý if I stop making these comments.
        /// </summary>

        public void JsonToParcel(string json)
        {
            CompanyDTO dto = new(json, Company);
            _model.CompletedParcel = dto.Completed;
            _model.CompletedParcel.URL = Url;
            Url = string.Empty;
        }

        /// <summary>
        /// This is how we handle the ID and turn it into the url we need, it currently is rough and unready, since we do not even handle the APIs.
        /// </summary>
        /// <returns> Eventually the completed url </returns>
        private string TurningIDToUrl()
        {
            DateTime anHourAgo = DateTime.Now.AddHours(-1);
            char[] idarray = ID.ToCharArray();
            switch (idarray.Take(2).ToString())
            {
                case ("MH"):
                    Company = "MH";
                    return $"HTTPS://extservicetest.matkahuolto.fi/mpaketti/public/tracking/?ids=<{ID}>&from={anHourAgo}>&to=<{DateTime.Now}>";  // Currently we are using the API meant for testing (found in their own documentation)
                default:
                    throw new ArgumentException("Could not find the firm");
            }
        }
    }
}