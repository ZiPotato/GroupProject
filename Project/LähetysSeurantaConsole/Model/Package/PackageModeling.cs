namespace LähetysSeurantaConsole.Model.Package
{
    internal class PackageModeling : IPackage
    {
        private static readonly HttpClient Client = new();
        public string ID { get; set; }
        public string Company;
        public string Url;
        IPackage _model;
        Parcel IPackage.LastParcel { get ; set ; }
        List<Parcel> IPackage.Parcels { get => new(); set; }

        /// <summary>
        /// Technically we should make it so we parse out the incorrectly formatted information out before this state
        /// but for the sake of testing and the origin I am doing it this way for the start atleast
        /// </summary>
        public PackageModeling(string iD, IPackage model)
        {
            _model = model;
            _model.Parcels = new List<Parcel>();
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

            CompanyDTO dto = new(json, Company);
            _model.LastParcel = dto.Completed;
            _model.Parcels.Add(_model.LastParcel);
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