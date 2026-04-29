using System.Dynamic;
using System.Threading.Tasks;

namespace LähetysSeurantaConsole.Model.Package
{
    /// <summary>
    /// Defines the contract for a package that manages a collection of parcels and provides operations to update parcel
    /// information from the presenter potentially.
    /// </summary>
    public interface IPackage
    {
        Task<Parcel> GetTheParcelAsync(string id);
        Task<Parcel> UpdateParcelAsync(Parcel par);
    }
}
