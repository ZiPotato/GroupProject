using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Package;
using System.Text.Json.Serialization;

namespace OrderTracking.Core.Models.Mapping.DTO;

public sealed class CompanyDTO
{
    public Parcel Completed { get; }

    /// <summary>
    /// Maps raw carrier JSON into a common <see cref="Parcel"/> model.
    /// </summary>
    /// <param name="json">Raw JSON from the carrier API.</param>
    /// <param name="company">Carrier identifier extracted from the tracking ID.</param>
    public CompanyDTO(string ID, string company)
    {
        string json = JsonHandle(company, ID);
        Completed = DTOHandle(json, company);
    }

    private static Parcel DTOHandle(string json, string company) =>
        company switch
        {
            "MH" => MatkahuoltoDTO.ToParcel(json),
            "JJ" => PostiDTO.ToParcel(json),
            _ => throw new ArgumentException($"Couldn't find the firm")
        };

    public static string JsonHandle(string company, string ID)
    {
       return company switch
        {
            "MH" => APIsimulation.SimulatingRandom(ID),
            "JJ" => PostiAPISimulation.SimulatingRandomPosti(ID),
            _ => throw new ArgumentException($"Couldn't find the firm")
        };
    }
}