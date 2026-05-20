using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Package;

namespace OrderTracking.Core.Models.Mapping.Tests;

[TestClass]
public sealed class CompanyDtoTests
{
    private const string DefaultTrackingId = "MH302164795FI";
    private const string DefaultUrl = "test-url";
    private static readonly DateTimeOffset ExpectedDeliveredAt =
        new(2014, 4, 14, 13, 35, 10, TimeSpan.FromHours(2));

    private PackageModeling _model = null!;
    private string _validJson = string.Empty;

    [TestInitialize]
    public void Initialize()
    {
        _validJson = LoadFixture("MH.json");
        _model = CreateModel();
    }

    [TestMethod]
    public void SimulatingRandom_WithMatkahuoltoPrefix_ReturnsJsonThatCanBeMapped()
    {
        var simulatedJson = APIsimulation.SimulatingRandom("MH");

        Assert.AreNotEqual(string.Empty, simulatedJson);

        var parcel = _model.JsonToParcel(simulatedJson);

        Assert.IsNotNull(parcel);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_ReturnsParcel()
    {
        var result = CreateParcel();

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_MapsTrackingId()
    {
        var result = CreateParcel();

        Assert.AreEqual(DefaultTrackingId, result.TrackingId);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_MapsCompany()
    {
        var result = CreateParcel();

        Assert.AreEqual("Matkahuolto", result.Company);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_MapsStatusDescription()
    {
        var result = CreateParcel();

        Assert.AreEqual("Delivered", result.StatusDescription);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_SetsDeliveryState()
    {
        var result = CreateParcel();

        Assert.IsTrue(result.IsDelivered);
        Assert.IsNotNull(result.DeliveredAt);
        Assert.AreEqual(ExpectedDeliveredAt, result.DeliveredAt);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_MapsSingleEvent()
    {
        var result = CreateParcel();

        Assert.AreEqual(1, result.Events.Count);
        Assert.AreEqual("Delivered (Additional Info)", result.Events[0].Description);
        Assert.AreEqual("HELSINKI", result.Events[0].Location);
        Assert.AreEqual(ExpectedDeliveredAt, result.Events[0].Timestamp);
    }

    [TestMethod]
    public void JsonToParcel_WithValidFixture_ToStringContainsExpectedSummary()
    {
        var result = CreateParcel();
        var par = RemoveWhitespace(result.ToString());

        Assert.AreEqual("ID:MH302164795FICarriercompany:MatkahuoltoCurrentstatus:DeliveredCurrentcity:HELSINKI", par);
    }

    [TestMethod]
    public void JsonToParcel_WithUrl_CopiesUrlToParcelAndClearsModelUrl()
    {
        var result = CreateParcel();

        Assert.AreEqual(DefaultUrl, result.URL);
        Assert.AreEqual(string.Empty, _model.Url);
    }

    [TestMethod]
    public void JsonToParcel_WithMalformedJson_ThrowsException()
    {
        const string invalidJson = """{ "MHTrackingEvents": { "Event": [ }""";

        Assert.Throws<Exception>(() => _model.JsonToParcel(invalidJson));
    }

    [TestMethod]
    public void JsonToParcel_WithNonJsonText_ThrowsException()
    {
        const string invalidJson = "this is not json at all";

        Assert.Throws<Exception>(() => _model.JsonToParcel(invalidJson));
    }

    [TestMethod]
    public void JsonToParcel_WithInvalidEventList_ReturnsParcel()
    {
        var invalidEventListJson = Regex.Replace( _validJson, @"""Event""\s*:\s*\[[\s\S]*?\]", @"""Event"": ""invalid-event-list""", RegexOptions.Singleline);

        var result = _model.JsonToParcel(invalidEventListJson);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void JsonToParcel_WithEventMissingLocation_ReturnsParcel()
    {
        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "Description": "In transit",
                    "Timestamp": "2014-04-14T13:35:10+02:00"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void JsonToParcel_WithEmptyEventArray_ReturnsParcelWithoutEvents()
    {
        const string json = """
            {
              "MHTrackingEvents": {
                "Event": []
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.AreEqual(0, result.Events.Count);
    }

    [TestMethod]
    public void JsonToParcel_WithNonDeliveredEvent_SetsIsDeliveredFalseAndDeliveredAtNull()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "15",
                    "EventTime": "2024-01-01T10:00:00+02:00",
                    "EventPlace": "TAMPERE"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.IsFalse(result.IsDelivered);
        Assert.IsNull(result.DeliveredAt);
    }

    [TestMethod]
    public void JsonToParcel_WithNullUrl_ReturnsParcel()
    {
        _model = CreateModel(url: null);

        var result = CreateParcel();

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void JsonToParcel_WhenCalledTwice_UsesCurrentUrlForEachMappedParcel()
    {
        _model = CreateModel(url: "first-url");
        var first = CreateParcel();

        _model.Url = "second-url";
        var second = CreateParcel();

        Assert.AreEqual("first-url", first.URL);
        Assert.AreEqual("second-url", second.URL);
    }

    [TestMethod]
    public void JsonToParcel_WithUnsupportedCompanyPrefix_ThrowsArgumentException()
    {
        _model = CreateModel("XX123456789");

        Assert.Throws<ArgumentException>(() => _model.JsonToParcel("{}"));
    }

    [TestMethod]
    public void JsonToParcel_WithTooShortTrackingId_ThrowsArgumentOutOfRangeException()
    {
        _model = CreateModel("M");

        Assert.Throws<ArgumentOutOfRangeException>(() => _model.JsonToParcel("{}"));
    }

    [TestMethod]
    public void JsonToParcel_WithSingleEventObject_MapsSingleEvent()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": {
                  "ShipmentNumber": "MH123456789FI",
                  "EventCode": "15",
                  "EventTime": "2024-01-01T10:00:00+02:00",
                  "EventPlace": "TAMPERE",
                  "Remarks": "Handled"
                }
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Events.Count);
        Assert.AreEqual("MH123456789FI", result.TrackingId);
        Assert.AreEqual("Received for transport (Handled)", result.Events[0].Description);
    }

    [TestMethod]
    public void JsonToParcel_WithUnsortedEvents_UsesLatestEventAsCurrentStatus()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "50",
                    "EventTime": "2024-01-01T12:00:00+02:00",
                    "EventPlace": "HELSINKI"
                  },
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "15",
                    "EventTime": "2024-01-01T08:00:00+02:00",
                    "EventPlace": "TURKU"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.AreEqual("Ready for pickup", result.StatusDescription);
        Assert.AreEqual(2, result.Events.Count);
        Assert.AreEqual("Received for transport", result.Events[0].Description);
        Assert.AreEqual("Ready for pickup", result.Events[1].Description);
        Assert.AreEqual("HELSINKI", result.Location);
    }

    [TestMethod]
    public void JsonToParcel_WhenShipmentNumberIsMissing_UsesParcelNumberAsTrackingId()
    {
        _model = CreateModel("MH000000000FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "ParcelNumber": "MH999999999FI",
                    "EventCode": "15",
                    "EventTime": "2024-01-01T08:00:00+02:00",
                    "EventPlace": "OULU"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.AreEqual("MH999999999FI", result.TrackingId);
    }

    [TestMethod]
    public void JsonToParcel_WithErrorPayload_ThrowsArgumentException()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Error": {
                  "ErrorCode": "401",
                  "ErrorText": "Invalid shipment number"
                }
              }
            }
            """;

        var exception = Assert.Throws<ArgumentException>(() => _model.JsonToParcel(json));

        StringAssert.Contains(exception.Message, "Matkahuolto returned an error");
    }

    [TestMethod]
    public void JsonToParcel_WithUnknownEventCode_UsesFallbackDescription()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "999",
                    "EventTime": "2024-01-01T08:00:00+02:00",
                    "EventPlace": "LAHTI"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.AreEqual("Unknown event code: 999", result.StatusDescription);
        Assert.AreEqual("Unknown event code: 999", result.Events[0].Description);
    }

    [TestMethod]
    public void JsonToParcel_WithMultipleDeliveryEvents_UsesLatestDeliveryTimestamp()
    {
        _model = CreateModel("MH123456789FI");

        const string json = """
            {
              "MHTrackingEvents": {
                "Event": [
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "60",
                    "EventTime": "2024-01-01T10:00:00+02:00",
                    "EventPlace": "HELSINKI"
                  },
                  {
                    "ShipmentNumber": "MH123456789FI",
                    "EventCode": "61",
                    "EventTime": "2024-01-01T14:30:00+02:00",
                    "EventPlace": "ESPOO"
                  }
                ]
              }
            }
            """;

        var result = _model.JsonToParcel(json);

        Assert.IsTrue(result.IsDelivered);
        Assert.AreEqual( new DateTimeOffset(2024, 1, 1, 14, 30, 0, TimeSpan.FromHours(2)), result.DeliveredAt);
    }

    private Parcel CreateParcel(string? json = null) => _model.JsonToParcel(json ?? _validJson);

    private static PackageModeling CreateModel(string trackingId = DefaultTrackingId, string? url = DefaultUrl) =>
        new()
        {
            ID = trackingId,
            Url = url
        };

    private static string LoadFixture(string fileName)
    {
        var path = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", fileName));

        return File.ReadAllText(path);
    }

    private static string RemoveWhitespace(string value) =>
        Regex.Replace(value, @"\s", string.Empty);
}