using LähetysSeurantaConsole.Model.Package;


namespace LähetysSeurantaConsole.View
{
    public interface IView
    {
        public string TrackingId { get; set; }
        public string Display { get; set; }

        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        event EventHandler AddPackage;
        event EventHandler DisplayLatestPackage;
        event EventHandler UserLogin;
    }
}
