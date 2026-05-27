using OrderTracking.Core.Models.Package;
using System.Text.RegularExpressions;
using System.Xml;
using OrderTracking.Core.Models.Package.Fetched;

namespace OrderTracking.Core.Validation
{
    public class TrackingIDValidation : ITrackingValidation
    {
        private PackageModeling model = new();
        private readonly FetchFileHandling fetch;

        public TrackingIDValidation() 
        {
            fetch = new FetchFileHandling();
        }

        public async Task<Parcel> ValidateParcelUpdate(Parcel par)
        {
            if (DateTime.Now - par.LastUpdated < TimeSpan.FromHours(1))
            {
                throw new Exception("It's been less than an hour from the last update");
            }
            var update = await model.UpdateParcelAsync(par);

            if (update.IsDelivered)
            {
                fetch.WriteDelivered(update);

                return update;
            }
            
            else
            {
                return await model.UpdateParcelAsync(par);
            }
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
                
                Parcel par = await model.GetTheParcelAsync(id);
                
                if (par.IsDelivered)
                {
                    fetch.WriteDelivered(par);
                }
                return par;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
