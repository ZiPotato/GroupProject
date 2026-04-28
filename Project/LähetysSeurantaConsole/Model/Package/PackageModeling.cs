using LähetysSeurantaConsole.Model.Package.API;
using LähetysSeurantaConsole.Model.Package.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package
{
    public class PackageModeling : IPackage
    {
        public string ID { get; set; } = string.Empty;
        public string Url = string.Empty;
        
        /// <summary>
        /// This is used to update the parcel information
        /// </summary>
        public async Task<Parcel> UpdateParcelAsync(Parcel par)
        {
            if (par.LastUpdated.Hour == DateTime.Now.Hour) 
            {
                Console.WriteLine("It's been less than an hour from the last update");
                return par;
            }
            ID = par.TrackingId;
            Url = par.URL;
            
            // string json = await _http.FindAndUseTheAPI(Url, ID);
            string json = APIsimulation.SimulationFromTheXML(ID);
            return JsonToParcel(json)?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        /// <summary>
        /// This is used to generate a new parcel
        /// </summary>
        public async Task<Parcel> GetTheParcelAsync(string id)
        {
            ID = id;
            Url = string.Empty;
            //string json = await _http.FindAndUseTheAPI(Url, ID);
            string json = APIsimulation.SimulatingRandom(ID);
            return JsonToParcel(json) ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }
        /// <summary>
        /// Here we turn the json file from the API / whatever first into a dto and then to a parcel.
        /// </summary>
        public Parcel JsonToParcel(string json)
        {
            CompanyDTO dto = new(json, ID[..2]);
            Parcel completed =  dto.Completed with { URL = Url };
            Url = string.Empty;
            return completed;
        }


    }
}