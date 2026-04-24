using LähetysSeurantaConsole.Presenter;

namespace LähetysSeurantaConsole.View
{
    internal class Program
    {
        /// <summary>
        /// Extremely simple "now we just run the code" code
        /// </summary>
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();
            new PackageIDHandling(view);
            while (view.running)
            {
                view.Menu();
            }
        }
    }
}
