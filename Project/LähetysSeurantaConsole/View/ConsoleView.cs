using OrderTracking.Core.Models.Package;
using Validation = OrderTracking.Core.Validation.TrackingIDValidation;

namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView
    {
        private readonly Validation _validation = new();
        private Parcel?[] parcels = new Parcel?[3];
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
                    AddParcel(_latest);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPackage retrieved successfully.\n");
                    Console.ResetColor();

                    PrintParcel();
                    break;

                case "2":
                    PrintParcel();
                    break;

                case "3":
                    await UpdateParcel();
                    break;

                case "0":
                    Running = false;
                    break;
            }
        }

        private async Task UpdateParcel()
        {
            if (parcels.All(p => p is null))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNo recent parcels yet. Add a tracking ID first.\n");
                Console.ResetColor();
                return;
            }

            UpdateOptions();

            int slot = ReadUpdateChoice();
            if (slot == 0)
            {
                return;
            }

            Parcel? selected = parcels[slot - 1];
            if (selected is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThat slot is empty.\n");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nTrying to update parcel '{selected.TrackingId}'...");
            Console.ResetColor();

            Parcel updated = await _validation.ValidateParcelUpdate(selected);
            _latest = updated;
            AddParcel(updated);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nParcel updated successfully.\n");
            Console.ResetColor();

            PrintParcel();
        }

        private void AddParcel(Parcel parcel)
        {
            for (int i = parcels.Length - 1; i > 0; i--)
            {
                parcels[i] = parcels[i - 1];
            }
            parcels[0] = parcel;
        }

        private void UpdateOptions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n------ Latest 3 Parcels ------");

            for (int i = 0; i < parcels.Length; i++)
            {
                Parcel? parcel = parcels[i];
                if (parcel is null)
                {
                    Console.WriteLine($"[{i + 1}] (empty)");
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] {parcel.TrackingId} | {parcel.Company} | {parcel.StatusDescription}");
                }
            }

            Console.WriteLine("------------------------------\n");
            Console.ResetColor();
        }

        private int ReadUpdateChoice()
        {
            while (true)
            {
                string input = ReadInput("Pick parcel slot [1-3] (or 0 to cancel): ");
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter 0, 1, 2, or 3.");
                Console.ResetColor();
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
            Console.WriteLine("[3] Update one of latest 3 packages");
            Console.WriteLine("[0] Exit");
            Console.ResetColor();
            Console.WriteLine();
        }

        private string ReadMenuChoice()
        {
            while (true)
            {
                string input = ReadInput("> ");
                if (input is "0" or "1" or "2" or "3")
                {
                    return input;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter 0, 1, 2, or 3.");
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