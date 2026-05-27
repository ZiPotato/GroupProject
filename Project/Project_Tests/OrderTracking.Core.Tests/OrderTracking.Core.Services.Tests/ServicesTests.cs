using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Services;

namespace OrderTracking.Core.Services.Tests;

// We still do need more of these tests, but since it's such of a new addition I really don't know how much deeper to go with it. - VP 
[TestClass]
public class ServicesTests
{
    private static Parcel CreateParcel(string trackingId, bool delivered = false) =>
        new()
        {
            TrackingId = trackingId,
            Company = "TestCompany",
            StatusDescription = "In transit",
            DeliveredAt = delivered ? DateTimeOffset.UtcNow : null,
            Events =
            [
                new ParcelEvent
                {
                    Timestamp = DateTimeOffset.UtcNow,
                    Description = "Created",
                    Location = "Siellä"
                }
            ]
        };

    [TestMethod]
    public void ParcelState_WhenCreated_CollectionsAreInitialized()
    {
        var state = new ParcelState();

        Assert.IsNotNull(state.ActiveParcels);
        Assert.IsNotNull(state.DeliveredParcels);
    }

    [TestMethod]
    public void ParcelState_WhenAddingActiveParcel_ItIsStoredInActiveParcels()
    {
        var state = new ParcelState();
        var parcel = CreateParcel("MH302164795FI", delivered: false);

        state.ActiveParcels.Add(parcel);

        // A cool feature btw
        CollectionAssert.Contains(state.ActiveParcels, parcel);
    }

    [TestMethod]
    public void ParcelState_WhenAddingDeliveredParcel_ItIsStoredInDeliveredParcels()
    {
        var state = new ParcelState();
        var parcel = CreateParcel("JJFI00000000000000", delivered: true);

        state.DeliveredParcels.Add(parcel);

        CollectionAssert.Contains(state.DeliveredParcels, parcel);
        Assert.IsTrue(parcel.IsDelivered);
    }
}