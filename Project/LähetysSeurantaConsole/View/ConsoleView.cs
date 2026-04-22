using System;
using System.Collections.Generic;
using System.Text;

namespace LähetysSeurantaConsole.View
{
    internal class ConsoleView : IView
    {
        public string TrackingId { get; set; }
        public string Display { get; set; }

        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }


        public bool AddPackage()
        {
            return true;
        }

        public bool DisplayLatestPackage()
        {
            return true;
        }

        public bool UserLogin()
        {
            return true;
        }
    }
}
