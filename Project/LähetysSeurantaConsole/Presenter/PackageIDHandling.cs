using LähetysSeurantaConsole.Model.Customer;
using LähetysSeurantaConsole.Model.Package;
using LähetysSeurantaConsole.View;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LähetysSeurantaConsole.Presenter
{
    internal class PackageIDHandling
    {
        // We Receive the ID from the customer and construct it into the desired url
        IView _view;
        IPackage _package;
        ICustomer _customer;

        public PackageIDHandling(IView view)
        {
            _view = view;
            _view.AddPackage += AddPackage;
            _view.DisplayLatestPackage += DisplayTheLatestPackage;
        }



        private void AddPackage(object sender, EventArgs e)
        {
            string iD = _view.Id;
            iD = iD.ToUpper().Trim();
            char[] iDarray = iD.ToCharArray();

            if (string.IsNullOrEmpty(iD)) throw new ArgumentNullException("ID cannot be null or empty");
            else if (!char.IsLetter(iDarray[0]) || !char.IsLetter(iDarray[1])) throw new ArgumentException("First two characters of a trackingnumber should be letters");

            Parcel p = _package.GetTheParcel(iD);
            
            if (p != null)
            {
                _customer.ParcelList.Add(p);
                Console.WriteLine("Success");
            }
            else Console.WriteLine("Something went wrong creating the parcel");
        }

        private void DisplayTheLatestPackage(object sender, EventArgs e)
        {
            if (_customer.ParcelList.Last() == null) throw new ArgumentNullException("The list of packages is empty.");
            Console.WriteLine(_customer.ParcelList.Last().ToString());
        }
    }
}
