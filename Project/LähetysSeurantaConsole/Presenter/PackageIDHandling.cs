using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace LähetysSeurantaConsole.Presenter
{
    internal class PackageIDHandling
    {
        // We Receive the ID from the customer and construct it into the desired url
        PackageIDHandling()
        {
            
        }

        private static string FirmHandling(string id)
        {
            char[] idarray = id.ToCharArray();
            string firmID = null;
            if (char.IsLetter(idarray[0]) && char.IsLetter(idarray[1])) firmID = idarray.Take(2).ToString();
            
            switch (firmID)
            {
                case (null): throw new ArgumentNullException();
                case ("FI"): return $"";                                                // Here goes the firm url potentially with the already altered uri elements
                default: throw new ArgumentException("Could not find the firm");
            }
                
        }
    }
}
