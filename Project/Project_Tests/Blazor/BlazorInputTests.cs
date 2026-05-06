using Bunit;
using OrderTrackingBlazor.Components.Pages;
using OrderTrackingBlazor.Components.Widget;
using TestContext = Bunit.TestContext;

namespace Project_Tests;

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
}
