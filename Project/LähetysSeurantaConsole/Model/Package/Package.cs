
namespace LähetysSeurantaConsole.Model.Package
{
    internal class Package
    {
        public string ID { get; set; }
        public string Company { get; set; }
        public bool Delivered { get; set; }
        public string State;
        public Package(string iD)
        {
            ID = iD;
            UpdateTheParcel();
        }
        public void UpdateTheParcel()
        {
            string url = IDHandling(ID);        // From here we seek to handle the data given to us by the url
            

        }

        private static string IDHandling(string id)
        {
            char[] idarray = id.ToCharArray();
            
            switch (idarray.Take(2).ToString())
            {
                case ("FI"): return $"HTTPS://Posti.Fi/Seuranta{id}";                                                // Here goes the firm url potentially with the already altered uri elements

                default: throw new ArgumentException("Could not find the firm");                                     // We probably will need to create our own exception class that will inform the user precicely the way we want
            }

        }
    }
}
