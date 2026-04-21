namespace LähetysSeurantaConsole.Model.Customer
{
    internal class Customer
    {
        // Here we give the customer a cookie so they can be logged in everytime they enter the site
        // And ofcourse if we do want to look at the "give me your Email" way, I guess it will work.

        public int Id { get; private set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(int id, string email="", string phoneNumber="")
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
