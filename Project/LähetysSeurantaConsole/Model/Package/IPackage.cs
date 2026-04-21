namespace LähetysSeurantaConsole.Model.Package
{
    /// <summary>
    /// Defines the contract for a package that manages a collection of parcels and provides operations to update parcel
    /// information from the presenter potentially.
    /// </summary>
    internal interface IPackage
    {
        Task GetTheParcel();
        Parcel CompletedParcel { get; set; }
    }
}
