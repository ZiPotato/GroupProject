
using OrderTracking.Core.Models.Package;
using CoreValidation = OrderTracking.Core.Validation.TrackingIDValidation;

namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView
    {
        public string? Id { get; set; }
        public bool running = true;
        public CoreValidation _validation = new();
        public Parcel? latest;
        /// <summary>
        /// One of the currently known bugs is that the menu does not wait for the API response to be printed before printing the menu again
        /// </summary>
        public async Task Menu()
        {
            PrintMenu();

            string input;
            do { input = ReadInput("> "); } while (!int.TryParse(input, out int _));
            try
            {
                if (input == "1")
                {
                    Id = ReadInput("ID: ");
                    latest = await _validation.ValidateNewTrackingId(Id);
                    Console.WriteLine($"\nPackage retrieved successfully: \n");
                    PrintParcel();
                }
                else if (input == "2") 
                {
                    PrintParcel();
                }
                else
                {
                    running = false;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error has occured: {ex.Message}\n");
                Console.ResetColor();
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
        private void PrintParcel()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{latest.ToString()}");
            Console.ResetColor();
        }
    }
}
