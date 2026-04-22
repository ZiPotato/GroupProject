using LähetysSeurantaConsole.Model.Customer;
using LähetysSeurantaConsole.Model.Package;
using LähetysSeurantaConsole.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.Presenter
{
    internal class CustomerHandling : ICustomer
    {
        IView _view;
        string ICustomer.Id { get; set; }
        string ICustomer.Name { get; set; }
        string ICustomer.Email { get ; set; }
        string ICustomer.Password { get; set;  } 
        List<Parcel> ICustomer.ParcelList { get ; set ; }
        string ICustomer.PhoneNumber { get ; set ; }
        ICustomer _customer;
        CustomerHandling(IView view)
        {
            _view = view;
            _view.UserLogin += UserLogin;
        }
        
        private void UserLogin(object sender, EventArgs e)
        {
            if (_customer.Password != null && _customer.Password == _view.Password) Console.WriteLine("Logged in successfully... I guess... Not implemented yet");
            else Console.WriteLine("Wrong password..."); // Here is where we'd put the account creation probably.
        }
    }
}
