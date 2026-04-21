using LähetysSeurantaConsole.Model.Package.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LähetysSeurantaConsole.Model.Package
{
    internal class CompanyDTO
    {
        public Parcel Completed { get; }

        public CompanyDTO(string json, string company)
        {
            Completed = DTOHandle(json, company);
        }

        private Parcel DTOHandle(string json, string company)
        {
            return company switch
            {
                "MA" => MatkahuoltoDTO.ToParcel(json),
                _ => throw new ArgumentException("Couldn't find the firm")
            };
        }
    }
}
