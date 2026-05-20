using OrderTracking.Core.Models.Package;
using System.Text.RegularExpressions;

namespace OrderTracking.Core.Validation
{
    public class TrackingIDValidation : ITrackingValidation
    {
        private PackageModeling model = new();

        public async Task<Parcel> ValidateParcelUpdate(Parcel par)
        {
            if (DateTime.Now - par.LastUpdated < TimeSpan.FromHours(1))
            {
                throw new Exception("It's been less than an hour from the last update");
            }

            return await model.UpdateParcelAsync(par);
        }

        public async Task<Parcel> ValidateNewTrackingId(string id)
        {
            try
            {
                id = id.ToUpper().Trim();
                char[] iDarray = id.ToCharArray();

                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("ID cannot be null or empty");
                if (!Regex.IsMatch(id, @"^[A-Z0-9]+$")) throw new ArgumentException("Tracking number cannot contain special characters");

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
