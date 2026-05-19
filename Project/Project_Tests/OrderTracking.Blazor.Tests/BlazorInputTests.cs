using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Validation;
using OrderTrackingBlazor.Components.Pages;
using OrderTrackingBlazor.Components.Widget;
using TestContext = Bunit.TestContext;

namespace Ordertracking.Blazor.Tests;


/// <summary>
/// Contains unit tests for verifying the behavior of the Blazor input and parcel widget components in the Home page.
/// </summary>
/// <remarks>These tests validate scenarios related to entering tracking IDs and interacting with the UI, ensuring
/// that only valid tracking IDs render the parcel widget and that updates to parcels occur as expected. The tests use
/// bUnit and MSTest frameworks to simulate user interactions and assert component rendering and state
/// changes. (These tests are not meant to be run often, but just after tweaks to the ParcelWidget) </remarks>

[TestClass]
public class BlazorInputTests
{
    private TestContext _context = null!;
    private Mock<ITrackingValidation> _valmock = null!;
    private IRenderedComponent<Home> _comp = null!;

    [TestInitialize]
    public void Initialize()
    {
        _context = new TestContext();
        _valmock = new Mock<ITrackingValidation>();

        _context.Services.AddSingleton(_valmock.Object);
        _comp = _context.RenderComponent<Home>();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    private static Parcel CreateParcel(
        string trackingId = "MH302164795FI",
        string company = "Matkahuolto",
        string status = "In transit",
        string location = "HELSINKI",
        DateTimeOffset? deliveredAt = null)
    {
        return new Parcel
        {
            TrackingId = trackingId,
            Company = company,
            StatusDescription = status,
            DeliveredAt = deliveredAt,
            Events =
            [
                new ParcelEvent
                {
                    Location = location,
                    Description = status,
                    Timestamp = DateTimeOffset.Now
                }
            ]
        };
    }

    private void AddTrackingId(string trackingId)
    {
        _comp.Find("input.ourtxt").Change(trackingId);
        _comp.Find("button.ourbtn").Click();
    }

    private void ClickParcelCard() => _comp.Find("article.parcel-card").Click();

    [TestMethod]
    public void ClickingAdd_WithValidTrackingId_RendersParcelWidget()
    {
        _valmock.Setup(v => v.ValidateNewTrackingId(It.IsAny<string>())).ReturnsAsync(CreateParcel());

        AddTrackingId("mh302164795fi");

        _comp.WaitForAssertion(() =>
        {
            Assert.AreEqual(1, _comp.FindComponents<ParcelWidget>().Count);
            StringAssert.Contains(_comp.Markup, "MH302164795FI");
        });

        _valmock.Verify(v => v.ValidateNewTrackingId(It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public void ClickingAdd_WithInvalidInput_DoesNotRenderParcelWidget()  // This was originally three tests, but since we don't need to test the actual function of the program here just the UI I just made it one invalid ID test.
    {
        _valmock.Setup(v => v.ValidateNewTrackingId(It.IsAny<string>())).ThrowsAsync(new Exception("Invalid tracking id"));

        AddTrackingId("321948");

        Assert.AreEqual(0, _comp.FindComponents<ParcelWidget>().Count);
        _valmock.Verify(v => v.ValidateNewTrackingId(It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public void ClickingAdd_WithTrackingIdContainingLowercaseLetters_DoesRenderParcelWidget()
    {
        _valmock.Setup(v => v.ValidateNewTrackingId(It.IsAny<string>())).ReturnsAsync(CreateParcel());

        AddTrackingId("mh302164795fi");

        Assert.AreEqual(1, _comp.FindComponents<ParcelWidget>().Count);
        _valmock.Verify(v => v.ValidateNewTrackingId(It.IsAny<string>()), Times.Once);
    }

    [TestMethod]
    public void UpdateParcel_ValidParcelWidget_TryingToUpdateParcelWhileItsStillTooNewToBeUpdated()
    {
        _valmock.Setup(v => v.ValidateNewTrackingId(It.IsAny<string>())).ReturnsAsync(CreateParcel(status: "In transit", location: "HELSINKI"));
        _valmock.Setup(v => v.ValidateParcelUpdate(It.IsAny<Parcel>())).ThrowsAsync(new Exception("Too soon to update"));

        AddTrackingId("mh302164795fi");
        ClickParcelCard();

        _comp.WaitForAssertion(() =>
        {
            StringAssert.Contains(_comp.Markup, "In transit");
            StringAssert.Contains(_comp.Markup, "HELSINKI");
        });

        _valmock.Verify(v => v.ValidateParcelUpdate(It.IsAny<Parcel>()), Times.Once);
    }

    [TestMethod]
    public void UpdateParcel_ValidParcelWidget_AfterClickingParcelWidgetItWillUpdateNormally()
    {
        _valmock.Setup(v => v.ValidateNewTrackingId(It.IsAny<string>())).ReturnsAsync(CreateParcel(status: "In transit", location: "HELSINKI"));
        _valmock.Setup(v => v.ValidateParcelUpdate(It.IsAny<Parcel>())).ReturnsAsync(CreateParcel(status: "Delivered", location: "TAMPERE", deliveredAt: DateTimeOffset.Now));

        AddTrackingId("mh302164795fi");
        ClickParcelCard();

        _comp.WaitForAssertion(() =>
        {
            StringAssert.Contains(_comp.Markup, "Delivered");
            StringAssert.Contains(_comp.Markup, "TAMPERE");
        });

        _valmock.Verify(v => v.ValidateParcelUpdate(It.IsAny<Parcel>()), Times.Once);
    }
}
