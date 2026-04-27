using LähetysSeurantaConsole.Model.Package.API;
using LähetysSeurantaConsole.Model.Package.DTO;
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

        /// <summary>
        /// This is used to update the parcel information
        /// </summary>
        public async Task<Parcel> UpdateParcelAsync(Parcel par)
        {
            ID = par.TrackingId;
            Url = par.URL;
            // string json = await _http.FindAndUseTheAPI(Url, ID);
            string json = APIsimulation.SimulationFromTheXML(ID);
            JsonToParcel(json);
            return CompletedParcel ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        /// <summary>
        /// This is used to generate a new parcel
        /// </summary>
        public async Task<Parcel> GetTheParcelAsync(string id)
        {
            ID = id;
            Url = string.Empty;
            //string json = await _http.FindAndUseTheAPI(Url, ID);
            string json = APIsimulation.SimulationFromTheXML(ID);
            JsonToParcel(json); // I've thought about just turning this into the parcel that is returned after the current changes. It wouldn't be that hard to make work, but I have been reluctant.
            return CompletedParcel ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }
        /// <summary>
        /// Here we turn the json file from the API / whatever first into a dto and then to a parcel.
        /// </summary>
        public void JsonToParcel(string json)
        {
            CompanyDTO dto = new(json, ID[..2]);
            CompletedParcel = dto.Completed with { URL = Url };
            Url = string.Empty;
        }


    }
}