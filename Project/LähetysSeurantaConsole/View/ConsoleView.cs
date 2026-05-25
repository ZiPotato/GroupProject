using OrderTracking.Core.Models.Package;
using Validation = OrderTracking.Core.Validation.TrackingIDValidation;

namespace LähetysSeurantaConsole.View
{
    /// <summary>
    /// Over designed user interface for our console application.
    /// </summary>
    internal class ConsoleView
    {
        private readonly Validation _validation = new();
        private readonly Parcel?[] _parcels = new Parcel?[3];
        private Parcel? _latest;

        public bool Running { get; private set; } = true;
        private static void WriteLineColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        /// <summary>
        /// Writes the line as blue
        /// </summary>
        private static void WriteInfo(string message) => WriteLineColored(message, ConsoleColor.Cyan);

        /// <summary>
        /// Writes the line as green
        /// </summary>
        private static void WriteSuccess(string message) => WriteLineColored(message, ConsoleColor.Green);

        /// <summary>
        /// Writes the line as yellow
        /// </summary>
        private static void WriteWarning(string message) => WriteLineColored(message, ConsoleColor.Yellow);

        /// <summary>
        /// Writes the line as red with a beep
        /// </summary>
        private static void WriteError(string message)
        {
            Console.Beep();
            WriteLineColored(message, ConsoleColor.Red);
        }

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
                WriteError($"\nAn error occurred: {ex.Message}\n");
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
                        WriteWarning("\nTracking ID cannot be empty.");
                        return;
                    }

                    WriteInfo("\nFetching package details...");

                    _latest = await _validation.ValidateNewTrackingId(trackingId);
                    AddParcel(_latest);

                    WriteSuccess("\nPackage retrieved successfully.\n");

                    PrintParcel();
                    break;

                case "2":
                    DisplayParcel();
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
            Parcel? selected = PickParcel();
            if (selected is null)
            {
                return;
            }

            WriteInfo($"\nTrying to update parcel '{selected.TrackingId}'...");

            Parcel updated = await _validation.ValidateParcelUpdate(selected);
            _latest = updated;
            AddParcel(updated);

            WriteSuccess("\nParcel updated successfully.\n");

            PrintParcel();
        }

        private void DisplayParcel()
        {
            Parcel? selected = PickParcel();
            if (selected is null)
            {
                return;
            }

            PrintParcel(selected);
        }

        private Parcel? PickParcel()
        {
            if (_parcels.All(p => p is null))
            {
                WriteWarning("\nNo recent parcels yet. Add a tracking ID first.\n");
                return null;
            }

            ParcelOptions();

            int slot = ReadParcelChoice();
            if (slot == 0)
            {
                return null;
            }

            Parcel? selected = _parcels[slot - 1];
            if (selected is null)
            {
                WriteWarning("\nThat slot is empty.\n");
                return null;
            }

            return selected;
        }

        private void AddParcel(Parcel parcel)
        {
            for (int i = _parcels.Length - 1; i > 0; i--)
            {
                _parcels[i] = _parcels[i - 1];
            }
            _parcels[0] = parcel;
        }

        private void ParcelOptions()
        {
            WriteLineColored("\n------ Latest 3 Parcels ------", ConsoleColor.Green);   // These technically can be written with WriteSuccess as well, but they're not success yet so it felt wrong.

            for (int i = 0; i < _parcels.Length; i++)
            {
                Parcel? parcel = _parcels[i];
                if (parcel is null)
                {
                    WriteLineColored($"[{i + 1}] (empty)", ConsoleColor.Green);
                }
                else
                {
                    WriteLineColored($"[{i + 1}] {parcel.TrackingId} | {parcel.Company} | {parcel.StatusDescription}", ConsoleColor.Green);
                }
            }

            WriteLineColored("------------------------------\n", ConsoleColor.Green);
        }

        private int ReadParcelChoice()
        {
            while (true)
            {
                string input = ReadInput("Pick parcel slot [1-3] (or 0 to cancel): ");
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= 3)
                {
                    return choice;
                }

                WriteWarning("Please enter 0, 1, 2, or 3.");
            }
        }

        private void PrintHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         PACKAGE TRACKING CONSOLE       ");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("[1] Add Tracking ID");
            Console.WriteLine("[2] Display package");
            Console.WriteLine("[3] Update package");
            Console.WriteLine("[0] Exit");
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

                WriteWarning("Please enter 0, 1, 2, or 3.");
            }
        }

        private string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        private void PrintParcel(Parcel? parcel = null)
        {
            Parcel? toDisplay = parcel ?? _latest;
            if (toDisplay is null)
            {
                WriteWarning("\nNo package loaded yet. Add a tracking ID first.\n");
                return;
            }

            WriteSuccess("\n----------- Latest Package -----------");
            WriteSuccess(toDisplay.ToString());
            WriteSuccess("--------------------------------------\n");
        }

        private void Pause()
        {
            WriteColored("Press Enter to continue...", ConsoleColor.DarkGray);
            Console.ReadLine();
        }

    }
}