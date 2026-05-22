using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace OrderTracking.Core.Models.Package.Fetched
{
    public class FetchFileHandling
    {
        private readonly string _root;

        public FetchFileHandling(string rootPath)
        {
            _root = rootPath;
        }
        public FetchFileHandling() : this(AppContext.BaseDirectory)
        {

        }

        public string DeliveredFilePath
        {
            get
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var directory = System.IO.Path.Combine(baseDir, "model", "package", "fetched");

                Directory.CreateDirectory(directory);

                return System.IO.Path.Combine(directory, "delivered.json");
            }
        }
        /// <summary>
        /// Writes the delivered parcel to a file in JSON format. If the file does not exist, it creates it first.
        /// </summary>
        /// <param name="delivered"></param>
        public void WriteDelivered(Parcel delivered)
        {
            string json = JsonConvert.SerializeObject(delivered);
            var file = DeliveredFilePath;
            if (!File.Exists(file))
            {
                CreateFile();
            }

            foreach (var line in File.ReadLines(file))
            {
                try
                {
                    var existing = JsonConvert.DeserializeObject<Parcel>(line);
                    if (existing != null && existing.TrackingId == delivered.TrackingId)
                        return;
                }
                catch { }
            }
            using (var writer = new StreamWriter(file, true))
            {
                writer.WriteLine(json);
            }
            else
            {
                CreateFile();
            }
        }
        public void CreateFile()
        {
            try
            {
                string? directory = System.IO.Path.GetDirectoryName(DeliveredFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                using (FileStream fs = File.Create(DeliveredFilePath))
                {
                    
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
            List<Parcel> Delivered = new();
            try
            {
                using (StreamReader reader = new StreamReader(DeliveredFilePath))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Parcel? parcel = JsonConvert.DeserializeObject<Parcel>(line);
                        if (parcel != null)
                        {
                            Delivered.Add(parcel);
                        }
                    }
                }
                return Delivered;
            }
            catch (DirectoryNotFoundException)
            {
                CreateFile();
                return Delivered;
            }
            catch (FileNotFoundException)
            {
                CreateFile();
                return Delivered;
            }
        }
        public void RemoveDelivered(string trackingId)
        {
            var all = ReadDelivered();
            all.RemoveAll(p => p.TrackingId == trackingId);

            File.WriteAllLines(DeliveredFilePath, all.Select(p => JsonConvert.SerializeObject(p)));
        }

        public bool FileExists()
        {
            return File.Exists(DeliveredFilePath);
        }
    }
}
