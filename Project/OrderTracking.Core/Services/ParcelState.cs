using System;
using System.Collections.Generic;
using System.Text;
using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Models.Package.Fetched;

namespace OrderTracking.Core.Services
{
    public class ParcelState
    {
        public List<Parcel> ActiveParcels { get; set; } = new();
        public List<Parcel> DeliveredParcels { get; set; } = new();

        private FetchFileHandling save = new();

        public void SaveParcel(Parcel parcel)
        {
            save.WriteDelivered(parcel);
        }

        public void RemoveParcel(Parcel parcel)
        {
            save.RemoveDelivered(parcel.TrackingId);
        }

        public ParcelState()
        {
            if (save.FileExists())
            {
                List<Parcel> list = save.ReadDelivered();

                foreach(Parcel parcel in list)
                {
                    if (parcel.IsDelivered)
                        DeliveredParcels.Add(parcel);
                    else
                        ActiveParcels.Add(parcel);
                }
            }
        }
    }
}
