using LähetysSeurantaConsole.Model.Package.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LähetysSeurantaConsole.Model.Package
{
    public class CompanyDTO
    {
        public Parcel Completed { get; }
        /// <summary>
        /// Currently we just have a singular company, but it will be easy to add more by creating a new DTO the same way.
        /// Originally I thought about creating all of the DTOs here, but decided against that after finishing the first one.
        /// </summary>
        /// <param name="json"> The raw string of Json data gathered from the API </param>
        /// <param name="company"> Company identifier that was previously extracted </param>
        public CompanyDTO(string json, string company)
        {
            Completed = DTOHandle(json, company);
        }

        private Parcel DTOHandle(string json, string company)
        {
            return company switch
            {
                "MH" => MatkahuoltoDTO.ToParcel(json),
                _ => throw new ArgumentException("Couldn't find the firm")
            };
        }
    }
}
