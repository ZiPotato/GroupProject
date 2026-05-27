using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Services;
using Validation = OrderTracking.Core.Validation.TrackingIDValidation;

namespace LähetysSeurantaConsole.Services
{
    internal sealed class ConsoleTrackingService
    {
        private readonly Validation _validation = new();

        internal Task<Parcel> AddIdAsync(string id)
        {
            return Fetch(() => _validation.ValidateNewTrackingId(id));
        }

        internal Task<Parcel> UpdateParcelAsync(Parcel par)
        {
            return Fetch(() => _validation.ValidateParcelUpdate(par));
        }
    
        internal List<Parcel> GetDeliveredParcels()
        {
            ParcelState state = new();
            return state.DeliveredParcels;
        }

        internal void ClearDeliveredParcels(IEnumerable<Parcel> delivered)
        {
            ParcelState state = new();
            foreach (Parcel par in delivered.ToList())
            {
                state.RemoveParcel(par);
            }
        }

        private async Task<Parcel> Fetch(Func<Task<Parcel>> action)
        {
            Parcel par = await action();

            if (par.IsDelivered)
            {
                ParcelState state = new();
                state.RemoveParcel(par);
                state.SaveParcel(par);
            }

            return par;
        }
    }
}