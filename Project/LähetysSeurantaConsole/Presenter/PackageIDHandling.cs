using LähetysSeurantaConsole.Model.Customer;
using LähetysSeurantaConsole.Model.Package;
using LähetysSeurantaConsole.View;

namespace LähetysSeurantaConsole.Presenter
{
    internal class PackageIDHandling
    {
        IView _view;
        IPackage _package;
        ICustomer _customer;        // All of customer is currently useless in our code

        public PackageIDHandling(IView view)
        {
            _view = view;
            _view.AddPackage += AddPackage;
            _view.DisplayLatestPackage += DisplayTheLatestPackage;
            _package = new PackageModeling();
        }

        private async void AddPackage(object sender, EventArgs e)
        {
            try
            {
                string iD = _view.Id;
                iD = iD.ToUpper().Trim();
                char[] iDarray = iD.ToCharArray();

                if (string.IsNullOrEmpty(iD)) throw new ArgumentNullException("ID cannot be null or empty");

                if (!char.IsLetter(iDarray[0]) && !char.IsLetter(iDarray[1]) || 
                    !char.IsLetter(iDarray[iDarray.Length - 1]) && !char.IsLetter(iDarray[iDarray.Length - 2]))
                {
                    throw new ArgumentException("Invalid tracking number");
                }

                Parcel p = await _package.GetTheParcelAsync(iD);
                // _customer.ParcelList.Add(p);
                Console.WriteLine($"Success\nParcel: \n{p}\nWas added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
        }

        private void DisplayTheLatestPackage(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(_customer.ParcelList.Last().ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"Fetching Parcel failed: {ex.Message}");
            }
        }
    }
}
