using System.Net.Http.Headers;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package
{
    public class PackageModeling : IPackage
    {

        private readonly HTTP _http = new();
        public string ID { get; set; } = string.Empty;
        public string Company = string.Empty;
        public string Url = string.Empty;
        public Parcel? CompletedParcel { get; set; }

        public async Task<Parcel> UpdateParcelAsync(Parcel par)
        {
            ID = par.TrackingId;
            Url = par.URL;
            string json = await _http.FindAndUseTheAPI(Url, ID);
            JsonToParcel(json);
            return CompletedParcel ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        public async Task<Parcel> GetTheParcelAsync(string id)
        {
            ID = id;
            Url = string.Empty;
            string json = await _http.FindAndUseTheAPI(Url, ID);
            JsonToParcel(json);
            return CompletedParcel ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        public void JsonToParcel(string json)
        {
            CompanyDTO dto = new(json, ID[..2]);
            CompletedParcel = dto.Completed with { URL = Url };
            Url = string.Empty;
        }


    }
}