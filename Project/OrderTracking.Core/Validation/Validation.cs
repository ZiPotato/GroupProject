using OrderTracking.Core.Models.Package;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderTracking.Core.Validation
{
    public class Validation
    {
        private PackageModeling model = new();

        public async Task<Parcel> UpdateValidation(Parcel par)
        {
            if (par.LastUpdated.Hour == DateTime.Now.Hour)
            {
                throw new Exception("It's been less than an hour from the last update");
            }
            else
            {
                return await model.UpdateParcelAsync(par);
            }
        }

        public async Task<Parcel> TrackingIDValidation(string id)
        {

            try
            {
                id = id.ToUpper().Trim();
                char[] iDarray = id.ToCharArray();

                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("ID cannot be null or empty");

                Regex reg = new Regex(@"\d");

                if (!char.IsLetter(iDarray[0]) && !char.IsLetter(iDarray[1]) ||
                    !char.IsLetter(iDarray[iDarray.Length - 1]) && !char.IsLetter(iDarray[iDarray.Length - 2]) ||
                    reg.Count(iDarray) <= 2)
                {
                    throw new ArgumentException("Invalid tracking number");
                }
                return await model.GetTheParcelAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
