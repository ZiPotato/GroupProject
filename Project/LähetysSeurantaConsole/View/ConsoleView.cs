using LähetysSeurantaConsole.Model.Package;


namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView : IView
    {
        public string TrackingId { get; set; }
        public string Display { get; set; }

        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }

        public List<Parcel> ParcelList { get; set; }


        public bool AddPackage()
        {
            TrackingId = ReadInput("Tracking ID: ");
            return true;
        }

        public bool DisplayLatestPackage()
        {
            // temp
            Parcel latestParcel = ParcelList.FirstOrDefault();
            ArgumentNullException.ThrowIfNull(latestParcel);
            Console.WriteLine(latestParcel);
            return true;
        }

        public bool UserLogin()
        {
            string input = ReadInput("Login: ");
            if (input.ToLower() == Email.ToLower())
                return true;

            // temp
            Console.WriteLine("Error: invalid email");
            return false;
        }

        private void PrintMenu()
        {
            Console.WriteLine("[1] Add Tracking ID\n" +
                              "[0] Exit\n");
        }

        private string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
