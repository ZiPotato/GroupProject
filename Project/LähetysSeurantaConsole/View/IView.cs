
using LähetysSeurantaConsole.Model.Package;


namespace LähetysSeurantaConsole.View
{
    public interface IView
    {
        
        public string Id { get;}
        
        event EventHandler AddPackage;
        event EventHandler DisplayLatestPackage;

    }
}
