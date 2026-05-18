using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace LähetysSeurantaConsole.Model.Package.Fetched
{
    internal class FetchFileHandling
    {
        public string Path
        {
            get {
                var path = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                return File.ReadAllText(path + "/model/package/fetched/delivered.json");
            }
        }
        /// <summary>
        /// Writes the delivered parcel to a file in JSON format. If the file does not exist, it creates it first.
        /// </summary>
        /// <param name="delivered"></param>
        public void WriteDelivered(Parcel delivered)
        {
            string json = JsonConvert.SerializeObject(delivered);
            try
            {
                using (StreamWriter writer = new StreamWriter(Path, true))
                {
                    writer.WriteLine(json);
                }
            }
            catch (FileNotFoundException file)
            {
                
                CreateFile();
                    using (StreamWriter writer = new StreamWriter(Path))
                    {
                        writer.WriteLine(json);
                    }
                
            }
        }
        private void CreateFile()
        {
            try
            {
                using (FileStream fs = File.Create(Path))
                {
                    File.Create(Path).Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the file: {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves all delivered parcels from the data source.
        /// </summary>
        public List<Parcel> ReadDelivered()
        {
            List<Parcel> delivered = new();
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Parcel? parcel = JsonConvert.DeserializeObject<Parcel>(line);
                        if (parcel != null)
                        {
                            delivered.Add(parcel);
                        }
                    }
                }
                return delivered;
            }
            catch (FileNotFoundException file)
            {
                CreateFile();
                return delivered;
            }
        }
    }
}
