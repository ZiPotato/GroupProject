namespace LähetysSeurantaConsole.View
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConsoleView view = new();

            while (view.Running)
            {
                Console.WriteLine("If you're able to read this, your console is slow or you're debugging...\nWhich is cheating!");
                await view.MenuAsync();
            }
        }
    }
}