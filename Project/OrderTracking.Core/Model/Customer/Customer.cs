using LähetysSeurantaConsole.Model.Package;

namespace LähetysSeurantaConsole.Model.Customer
{
    internal class Customer : ICustomer
    {
        // Here we give the customer a cookie so they can be logged in everytime they enter the site
        // And ofcourse if we do want to look at the "give me your Email" way, I guess it will work.

        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Parcel> ParcelList { get; set; }
        public Customer(string id, string email="", string phoneNumber="", string name="")
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            Name = name;
            ParcelList = new();
        }
    }
}
