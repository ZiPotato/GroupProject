using LähetysSeurantaConsole.Modeling;
using LähetysSeurantaConsole.Services;
using OrderTracking.Core.Models.Package;

namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView
    {
        private readonly ConsoleTrackingService _service = new();
        private readonly RecentParcels _recent = new();
        private readonly Dictionary<string, Func<Task>> _menu;

        private Parcel? _latest;

        public bool Running { get; private set; } = true;

        public ConsoleView()
        {
            _menu = new Dictionary<string, Func<Task>>
            {
                ["1"] = AddTrackingIdAsync,
                ["2"] = () => { DisplayParcel(); return Task.CompletedTask; },
                ["3"] = UpdateParcelAsync,
                ["4"] = () => { PrintDeliveredParcels(); return Task.CompletedTask; },
                ["0"] = () => { Running = false; return Task.CompletedTask; }
            };
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
                Style.WriteError($"\nAn error occurred: {ex.Message}\n");
            }

            if (Running)
            {
                Pause();
            }
        }

        private Task HandleChoiceAsync(string input)
        {
            if (_menu.TryGetValue(input, out Func<Task>? action))
            {
                return action();
            }

            Style.WriteWarning("Unknown menu option.");
            return Task.CompletedTask;
        }

        private async Task AddTrackingIdAsync()
        {
            string id = ReadInput("Enter tracking ID: ");
            if (string.IsNullOrWhiteSpace(id))
            {
                Style.WriteWarning("\nTracking ID cannot be empty.");
                return;
            }

            Style.WriteInfo("\nFetching package details...");

            Parcel parcel = await _service.AddIdAsync(id);
            HandleParcelResult(parcel, "\nPackage retrieved successfully.\n");
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

        private async Task UpdateParcelAsync()
        {
            Parcel? selected = PickParcel();
            if (selected is null)
            {
                return;
            }

            Style.WriteInfo($"\nTrying to update package '{selected.TrackingId}'...");

            Parcel updated = await _service.UpdateParcelAsync(selected);
            HandleParcelResult(updated, "\nPackage updated successfully.\n");
        }

        private void HandleParcelResult(Parcel parcel, string successMessage)
        {
            _latest = parcel;
            _recent.Add(parcel);

            Style.WriteSuccess(successMessage);
            PrintParcel(parcel);
        }

        private void PrintDeliveredParcels()
        {
            List<Parcel> delivered = _service.GetDeliveredParcels();

            if (delivered.Count == 0)
            {
                Style.WriteWarning("\nNo delivered parcels yet.\n");
                return;
            }

            Style.WriteSuccess("\n----------- Delivered Parcels -----------");
            foreach (Parcel par in delivered)
            {
                Style.WriteSuccess(par.ToString());
                Style.WriteSuccess("-----------------------------------------");
            }

            string choice = ReadInput("Clear all delivered parcels? (Y/N): ").Trim();
            if (choice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                _service.ClearDeliveredParcels(delivered);
                Style.WriteSuccess("\nAll delivered parcels were cleared.\n");
            }

            Style.WriteSuccess(string.Empty);
        }

        private Parcel? PickParcel()
        {
            if (_recent.IsEmpty)
            {
                Style.WriteWarning("\nNo recent package yet. Add a tracking ID first.\n");
                return null;
            }

            PrintRecentParcelOptions();

            int slot = ReadParcelChoice();
            if (slot == 0)
            {
                return null;
            }

            Parcel? selected = _recent.GetBySlot(slot);
            if (selected is null)
            {
                Style.WriteWarning("\nThat slot is empty.\n");
                return null;
            }

            return selected;
        }

        private void PrintHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         PACKAGE TRACKING CONSOLE       ");
            Console.WriteLine("========================================");
        }

        private void PrintMenu()
        {
            Console.WriteLine(
                    "\nChoose an action:\n" +
                    "[1] Add Tracking ID\n" +
                    "[2] Display package\n" +
                    "[3] Update package\n" +
                    "[4] Print delivered parcels\n" +
                    "[0] Exit\n");
        }

        private string ReadMenuChoice()
        {
            while (true)
            {
                string input = ReadInput("> ");
                if (input is "0" or "1" or "2" or "3" or "4")
                {
                    return input;
                }

                Style.WriteWarning("Please enter 0, 1, 2, 3, or 4.");
            }
        }

        private void PrintRecentParcelOptions()
        {
            Style.WriteSuccess("\n------ Latest 3 Parcels ------");

            foreach ((int slot, Parcel? parcel) in _recent.GetSlots())
            {
                if (parcel is null)
                {
                    Style.WriteWarning($"[{slot}] (empty)");
                }
                else
                {
                    Style.WriteSuccess($"[{slot}] {parcel.TrackingId} | {parcel.Company} | {parcel.StatusDescription}");
                }
            }

            Style.WriteSuccess("------------------------------\n");
        }

        private int ReadParcelChoice()
        {
            while (true)
            {
                string input = ReadInput($"Pick parcel slot [1-{_recent.Capacity}] (or 0 to cancel): ");
                if (int.TryParse(input, out int choice) && choice >= 0 && choice <= _recent.Capacity)
                {
                    return choice;
                }

                Style.WriteWarning("Please enter a valid slot.");
            }
        }

        private string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        private void PrintParcel(Parcel? par = null)
        {
            Parcel? toDisplay = par ?? _latest;
            if (toDisplay is null)
            {
                Style.WriteWarning("\nNo package loaded yet. Add a tracking ID first.\n");
                return;
            }

            Style.WriteSuccess("\n----------- Latest Package -----------");
            Style.WriteSuccess(toDisplay.ToString());
            Style.WriteSuccess("--------------------------------------\n");
        }

        private void Pause()
        {
            Style.WriteHint("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}