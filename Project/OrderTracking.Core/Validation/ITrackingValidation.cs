
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Validation
{
    public interface ITrackingValidation
    {
        Task<Parcel> ValidateParcelUpdate(Parcel par);
        Task<Parcel> ValidateNewTrackingId(string id);
    }
}