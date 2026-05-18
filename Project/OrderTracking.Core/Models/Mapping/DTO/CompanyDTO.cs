using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Models.Mapping.DTO;

public sealed class CompanyDTO
{
    public Parcel Completed { get; }

    /// <summary>
    /// Maps raw carrier JSON into a common <see cref="Parcel"/> model.
    /// </summary>
    /// <param name="json">Raw JSON from the carrier API.</param>
    /// <param name="company">Carrier identifier extracted from the tracking ID.</param>
    public CompanyDTO(string json, string company)
    {
        Completed = DTOHandle(json, company);
    }

    private static Parcel DTOHandle(string json, string company) =>
        company switch
        {
            "MH" => MatkahuoltoDTO.ToParcel(json),
            "JJ" => PostiDTO.ToParcel(json),
            _ => throw new ArgumentException($"Couldn't find the firm")
        };
}