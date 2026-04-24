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
        public string Password { get; set; }
        public bool running = true;

        public event EventHandler AddPackage;
        public event EventHandler DisplayLatestPackage;
        public event EventHandler UserLogin;
        /// <summary>
        /// One of the currently known bugs is that the menu does not wait for the API response to be printed before printing the menu again
        /// </summary>
        public void Menu()
        {
            PrintMenu();

            string input;
            do { input = ReadInput("> "); } while (!int.TryParse(input, out int _));

            if (input == "1")
            {
                Id = ReadInput("ID: ");
                AddPackage?.Invoke(this, EventArgs.Empty);
            }
            else if (input == "2") 
            {
                DisplayLatestPackage?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                running = false;
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("[2] Display the latest package\n" +
                              "[1] Add Tracking ID\n" +
                              "[0] Exit\n");
        }

        private string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
