using Bunit;
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

    [TestMethod]
    public void ClickingAdd_WithValidTrackingId_RendersParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("  mh302164795fi ");
        obj.Find("button.ourbtn").Click();

        obj.WaitForAssertion(() =>
        {
            Assert.AreEqual(1, obj.FindComponents<ParcelWidget>().Count);
            StringAssert.Contains(obj.Markup, "MH302164795FI");
        });
    }

    [TestMethod]
    public void ClickingAdd_WithInvalidTrackingId_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("12345");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithEmptyTrackingId_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithWhitespaceTrackingId_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("   ");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithTrackingIdMissingLetters_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("302164795fi");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    } 
    [TestMethod]
    public void ClickingAdd_WithTrackingIdMissingLettersAtTheEnd_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("MH302164795");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithTrackingIdContainingSpecialCharacters_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("MH302164795@!");
        obj.Find("button.ourbtn").Click();
        
        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithTrackingIdContainingOnlyLetters_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("MHFI");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithTrackingIdContainingOnlyNumbers_DoesNotRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("302164795");
        obj.Find("button.ourbtn").Click();

        Assert.AreEqual(0, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void ClickingAdd_WithTrackingIdContainingLowercaseLetters_DoesRenderParcelWidget()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();
        obj.Find("input.ourtxt").Change("mh302164795fi");
        obj.Find("button.ourbtn").Click();
        Assert.AreEqual(1, obj.FindComponents<ParcelWidget>().Count);
    }
    [TestMethod]
    public void UpdateParcel_ValidParcelWidget_TryingToUpdateParcelWhileItsStillTooNewToBeUpdated()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("mh302164795fi");
        obj.Find("button.ourbtn").Click();


        var widget = obj.FindComponent<ParcelWidget>();
        var before = widget.Instance.Parcel.ToString();

        obj.Find("article.parcel-card").Click();
       
        obj.WaitForAssertion(() =>
        {
            var after = obj.FindComponent<ParcelWidget>().Instance.Parcel.ToString();
            Assert.AreEqual(before, after);
        });
    }
    [TestMethod]
    public void UpdateParcel_ValidParcelWidget_AfterClickingParcelWidgetItWillUpdateNormally()
    {
        using var context = new TestContext();
        var obj = context.RenderComponent<Home>();

        obj.Find("input.ourtxt").Change("mh302164795fi");
        obj.Find("button.ourbtn").Click();


        var widget = obj.FindComponent<ParcelWidget>();
        var before = widget.Instance.Parcel.ToString();

        widget.Instance.Parcel.LastUpdated = DateTime.Now.AddHours(-2);

        obj.Find("article.parcel-card").Click();

        obj.WaitForAssertion(() =>
        {
            var after = obj.FindComponent<ParcelWidget>().Instance.Parcel.ToString();
            Assert.AreNotEqual(before, after);
        });
    }
}
