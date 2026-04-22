using LähetysSeurantaConsole.Model.Package;
using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.Model.Customer
{
    internal interface ICustomer
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public List<Parcel> ParcelList { get; set; }
    }
}
