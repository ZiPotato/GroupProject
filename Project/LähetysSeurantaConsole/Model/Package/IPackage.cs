using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package
{
    internal interface IPackage
    {
        Task UpdateTheParcel();
        List<Parcel> Parcels { get ; set; }
        Parcel LastParcel { get; set; }
    }
}
