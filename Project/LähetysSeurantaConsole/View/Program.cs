using LähetysSeurantaConsole.Presenter;

namespace LähetysSeurantaConsole.View
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();
            new PackageIDHandling(view);
            view.Menu();
        }
    }
}
