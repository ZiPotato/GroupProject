using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package.API
{
    public class APIsimulation
    {
        /// <summary>
        /// This is used to simulate retrieving their own test data from a given API.
        /// This doesn't actually use the ID for anything other than choosing the company.
        /// </summary>
        public static string SimulationFromTheXML(string id)
        {
            switch (id[..2])
            {
                case "MH":
                    {
                        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        return File.ReadAllText(path + "/model/package/api/MH.json");
                    }
                default:
                    throw new ArgumentException("Couldn't find a firm");
            }   
        }

        public static string SimulatingRandom(string id)
        {




            return "";
        }
    }
}
