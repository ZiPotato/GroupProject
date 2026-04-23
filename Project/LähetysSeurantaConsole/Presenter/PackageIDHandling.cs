using LähetysSeurantaConsole.Model.Customer;
using LähetysSeurantaConsole.Model.Package;
using LähetysSeurantaConsole.View;

namespace LähetysSeurantaConsole.Presenter
{
    internal class PackageIDHandling
    {
        IView _view;
        IPackage _package;
        ICustomer _customer;

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
                if (!char.IsLetter(iDarray[0]) || !char.IsLetter(iDarray[1])) throw new ArgumentException("First two characters of a trackingnumber should be letters");

                Parcel p = await _package.GetTheParcelAsync(iD);

                if (p != null)
                {
                    _customer.ParcelList.Add(p);
                    Console.WriteLine($"Success\nParcel: {p}\nWas added.");
                }
                else
                {
                    Console.WriteLine("Something went wrong creating the parcel");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
            }
        }

        private void DisplayTheLatestPackage(object sender, EventArgs e)
        {
            if (_customer.ParcelList == null || _customer.ParcelList.Last() == null)
            {
                throw new ArgumentNullException("The last package doesn't seem to exist.");
            }

            Console.WriteLine(_customer.ParcelList.Last().ToString());
        }
    }
}
