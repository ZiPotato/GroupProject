using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Validation;

namespace OrderTracking.Core.Validation.Tests;

[TestClass]
public class NewTrackingIDValidationTests
{
    public TrackingIDValidation _validation;
    [TestInitialize]
    public void SetUp()
    {
        _validation = new();
    }
    /// <summary>
    /// These Are the tests that test New tracking ID
    /// </summary>
    [TestMethod]
    public async Task ValidateNewTrackingID_ValidID_NewNormalParcel()
    {
        Assert.IsNotNull(await _validation.ValidateNewTrackingId("Mh23914fi"));
    }
    [TestMethod]
    public async Task ValidateNewTrackingId_IDThatDoesntContainLetters_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("31804731"));
    }
    [TestMethod]
    public async Task ValidateNewTrackingId_IDThatDoesntContainAnyNumbersButContainsTheCorrectLetters_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("MHFI"));
    }
    [TestMethod]
    public async Task ValidateNewTrackingId_IDThatDoesntMatchTheCompaniesWeCurrentlyHaveImplemented_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("Uff2315964fi"));
    }
    [TestMethod]
    public async Task ValidateNewTrackingId_IDThatHasAnImplementedCompanyButDoesntHaveTheCountryCode()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("MH3218597"));
    }
    [TestMethod]
    public async Task ValidateNewTrackignId_Null_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId(null));
    }
    [TestMethod]
    public async Task ValidateNewTrackingID_StringEmpty_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId(string.Empty));
    }
    [TestMethod]
    public async Task ValidateNewParcelTrackingId_IDContainsSpecialCharactersThatShouldntBeThere_ThrowsAnException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("MH321!!!49312Fi"));
    }
    [TestMethod]
    public async Task ValidateNewTrackingId_LeadingWhitespace_ThrowsException()
    {
        Parcel par = await _validation.ValidateNewTrackingId(("  MH302164795FI"));
        Assert.IsNotNull(par);
    }

    [TestMethod]
    public async Task ValidateNewTrackingId_TrailingWhitespace_ThrowsException()
    {
        Parcel par = await _validation.ValidateNewTrackingId("MH302164795FI  ");
        Assert.IsNotNull(par);
    }

    [TestMethod]
    public async Task ValidateNewTrackingId_InternalWhitespace_ThrowsException()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _validation.ValidateNewTrackingId("MH 302164795 FI"));
    }

    [TestMethod]
    public async Task ValidateNewTrackingId_ValidLowercase_ReturnsNormalizedUppercase()
    {
        Parcel result = await _validation.ValidateNewTrackingId("mh302164795fi");
        Assert.AreEqual("MH302164795FI", result.TrackingId);
    }
}
