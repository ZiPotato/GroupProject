using OrderTracking.Core.Models.Package;
using CoreValidation = OrderTracking.Core.Validation.TrackingIDValidation;

namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView
    {
        private readonly CoreValidation _validation = new();
        private Parcel? _latest;

        public bool Running { get; private set; } = true;

        public async Task MenuAsync()
        {
            Console.Clear();
            PrintHeader();
            PrintMenu();

            string input = ReadMenuChoice();

            try
            {
                await HandleChoiceAsync(input);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nAn error occurred: {ex.Message}\n");
                Console.ResetColor();
            }

            if (Running)
            {
                Pause();
            }
        }

        private async Task HandleChoiceAsync(string input)
        {
            switch (input)
            {
                case "1":
                    string trackingId = ReadInput("Enter tracking ID: ");
                    if (string.IsNullOrWhiteSpace(trackingId))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nTracking ID cannot be empty.");
                        Console.ResetColor();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nFetching package details...");
                    Console.ResetColor();

                    _latest = await _validation.ValidateNewTrackingId(trackingId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPackage retrieved successfully.\n");
                    Console.ResetColor();

                    PrintParcel();
                    break;

                case "2":
                    PrintParcel();
                    break;

                case "0":
                    Running = false;    // Here can be added a check for "Are you sure" or something like that, but I honestly hate those 80% of the time so I am not adding one now.
                    break;
            }
        }

        private void PrintHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         PACKAGE TRACKING CONSOLE       ");
            Console.WriteLine("========================================");
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("[1] Add Tracking ID");
            Console.WriteLine("[2] Display Latest Package");
            Console.WriteLine("[0] Exit");
            Console.ResetColor();
            Console.WriteLine();
        }

        private string ReadMenuChoice()
        {
            while (true)
            {
                string input = ReadInput("> ");
                if (input is "0" or "1" or "2")
                {
                    return input;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter 0, 1, or 2.");
                Console.ResetColor();
            }
        }

        private string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        private void PrintParcel()
        {
            if (_latest is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo package loaded yet. Add a tracking ID first.\n");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n----------- Latest Package -----------");
            Console.WriteLine(_latest);
            Console.WriteLine("--------------------------------------\n");
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Press Enter to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}