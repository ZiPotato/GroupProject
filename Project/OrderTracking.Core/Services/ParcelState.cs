using System;
using System.Collections.Generic;
using System.Text;
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Services
{
    public class ParcelState
    {
        public List<Parcel> ActiveParcels { get; set; } = new();
        public List<Parcel> DeliveredParcels { get; set; } = new();


    }
}
