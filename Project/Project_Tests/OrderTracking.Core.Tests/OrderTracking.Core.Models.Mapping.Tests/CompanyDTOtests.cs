using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Models.Mapping.Tests;

[TestClass]
public sealed class CompanyDtoTests
{
    private const string DefaultTrackingId = "MH302164795FI";
    private const string DefaultUrl = "test-url";

    private PackageModeling _model = null!;

    [TestInitialize]
    public void Initialize()
    {
        _model = CreateModel();
    }

    [TestMethod]
    public void SimulatingRandom_WithMatkahuoltoId_ReturnsNonEmptyJson()
    {
        var simulatedJson = APIsimulation.SimulatingRandom(DefaultTrackingId);

        Assert.IsFalse(string.IsNullOrWhiteSpace(simulatedJson));
    }

    [TestMethod]
    public void JsonToParcel_WithValidMatkahuoltoTrackingId_ReturnsParcel()
    {
        var result = _model.JsonToParcel();

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void JsonToParcel_WithValidMatkahuoltoTrackingId_MapsTrackingId()
    {
        var result = _model.JsonToParcel();

        Assert.AreEqual(DefaultTrackingId, result.TrackingId);
    }

    [TestMethod]
    public void JsonToParcel_WithValidMatkahuoltoTrackingId_MapsCompany()
    {
        var result = _model.JsonToParcel();

        Assert.AreEqual("Matkahuolto", result.Company);
    }

    [TestMethod]
    public void JsonToParcel_WithUrl_CopiesUrlToParcelAndClearsModelUrl()
    {
        var result = _model.JsonToParcel();

        Assert.AreEqual(DefaultUrl, result.URL);
        Assert.AreEqual(string.Empty, _model.Url);
    }

    [TestMethod]
    public void JsonToParcel_WithNullUrl_ReturnsParcelAndClearsModelUrl()
    {
        _model = CreateModel(url: null);

        var result = _model.JsonToParcel();

        Assert.IsNotNull(result);
        Assert.IsNull(result.URL);
        Assert.AreEqual(string.Empty, _model.Url);
    }

    [TestMethod]
    public void JsonToParcel_WhenCalledTwice_UsesCurrentUrlForEachMappedParcel()
    {
        _model = CreateModel(url: "first-url");
        var first = _model.JsonToParcel();

        _model.Url = "second-url";
        var second = _model.JsonToParcel();

        Assert.AreEqual("first-url", first.URL);
        Assert.AreEqual("second-url", second.URL);
    }

    [TestMethod]
    public void JsonToParcel_WithUnsupportedCompanyPrefix_ThrowsArgumentException()
    {
        _model = CreateModel("XX123456789");

        Assert.Throws<ArgumentException>(() => _model.JsonToParcel());
    }

    [TestMethod]
    public void JsonToParcel_WithTooShortTrackingId_ThrowsArgumentOutOfRangeException()
    {
        _model = CreateModel("M");

        Assert.Throws<ArgumentOutOfRangeException>(() => _model.JsonToParcel());
    }

    private static PackageModeling CreateModel(string trackingId = DefaultTrackingId, string? url = DefaultUrl) =>
        new()
        {
            company = trackingId,
            Url = url!
        };
}