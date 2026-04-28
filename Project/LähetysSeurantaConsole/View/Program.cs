using LähetysSeurantaConsole.Model.Customer;
using LähetysSeurantaConsole.Presenter;

namespace LähetysSeurantaConsole.View
{
    internal class Program
    {
        /// <summary>
        /// Extremely simple "now we just run the code" code
        /// 
        /// If we want to add the customer information to the console application we should put it here.
        /// </summary>
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView();

            // We should ask for the customer information here and create the customerhandling and the customer while creating the IDHandling.

            new PackageIDHandling(view);
            while (view.running)
            {
                view.Menu();
            }
        }
    }
}
