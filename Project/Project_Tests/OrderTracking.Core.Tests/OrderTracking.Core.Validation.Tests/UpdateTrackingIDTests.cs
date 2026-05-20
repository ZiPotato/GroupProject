using Microsoft.AspNetCore.Components;
using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Validation;
namespace OrderTracking.Core.Validation.Tests;

[TestClass]
public class UpdateTrackingIDTests
{
    public TrackingIDValidation _validation;
    public Parcel par;
    [TestInitialize]
    public async Task SetUp()
    {
        _validation = new();
        par = await _validation.ValidateNewTrackingId("MH1235678FI");
    }
    [TestMethod]
    public async Task ValidateParcelUpdate_ParcelThatWasCreatedLessThanAnHourAgo_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateParcelUpdate(par));
    }
    [TestMethod]
    public async Task ValidateParcelUpdate_ParcelThatWasCreatedAnHourAgo_ANewlyGeneratedParcelAsync()
    {
        par.LastUpdated = par.LastUpdated.AddHours(-1);
        Assert.AreNotEqual(par, await _validation.ValidateParcelUpdate(par)); // And before you think "what if there's bad luck and they are currently similar? They cannot be the same if the update worked because the last updated will have been updated.
    }
    [TestMethod]
    public async Task ValidateParcelUpdate_Null_WillThrowAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateParcelUpdate(par));
    }
    [TestMethod]
    public async Task ValidateParcelUpdate_NullParcel_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateParcelUpdate(null));
    }

    [TestMethod]
    public async Task ValidateParcelUpdate_Parcel59Minutes59SecondsOld_ThrowsException()
    {
        par.LastUpdated = DateTime.Now.AddMinutes(-59).AddSeconds(-59);
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateParcelUpdate(par));
    }

    [TestMethod]
    public async Task ValidateParcelUpdate_ParcelExactly60MinutesOld_ReturnsUpdatedParcel()
    {
        par.LastUpdated = DateTime.Now.AddMinutes(-60);
        Parcel result = await _validation.ValidateParcelUpdate(par);
        Assert.IsNotNull(result);
        Assert.AreNotEqual(par.LastUpdated, result.LastUpdated);
    }

    [TestMethod]
    public async Task ValidateParcelUpdate_ParcelOver60MinutesOld_ReturnsUpdatedParcel()
    {
        par.LastUpdated = DateTime.Now.AddHours(-2);
        Parcel result = await _validation.ValidateParcelUpdate(par);
        Assert.IsNotNull(result);
    }
}
