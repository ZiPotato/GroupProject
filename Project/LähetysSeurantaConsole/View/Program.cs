namespace LähetysSeurantaConsole.View
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();
            while (view.running)
            {
                view.Menu();
            }
        }
    }
}
