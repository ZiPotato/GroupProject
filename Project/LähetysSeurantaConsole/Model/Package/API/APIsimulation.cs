using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LähetysSeurantaConsole.Model.Package.API
{
    public class APIsimulation
    {
        public static string SimulationFromTheXML(string id)
        {
            switch (id[..2])
            {
                case "MH":
                    {
                        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                        return File.ReadAllText(path + "/model/package/api/jsontest.json");
                    }
                default:
                    throw new ArgumentException("Couldn't find a firm");
            }
            
        }
    }
}
