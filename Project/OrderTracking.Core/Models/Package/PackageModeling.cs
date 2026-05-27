using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Mapping.DTO;
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Models.Package
{
    public class PackageModeling
    {
        public string company { get; set; } = string.Empty;
        public string Url = string.Empty;

        /// <summary>
        /// This is used to update the parcel information
        /// </summary>
        public async Task<Parcel> UpdateParcelAsync(Parcel par)
        {
            if (par.LastUpdated.Hour == DateTime.Now.Hour)
            {
                throw new Exception("It's been less than an hour from the last update");
            }

            company = par.TrackingId;
            Url = par.URL ?? string.Empty;

            return JsonToParcel() ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        /// <summary>
        /// This is used to generate a new parcel
        /// </summary>
        public async Task<Parcel> GetTheParcelAsync(string id)
        {
            company = id;
            Url = string.Empty;

            return JsonToParcel() ?? throw new InvalidOperationException("Parcel was not created from the API response.");
        }

        /// <summary>
        /// Here we turn the json file from the API / whatever first into a dto and then to a parcel.
        /// </summary>
        public Parcel JsonToParcel()
        {
            CompanyDTO dto = new(company, company[..2]);
            Parcel completed = dto.Completed with { URL = Url };
            Url = string.Empty;
            return completed;
        }
    }
}