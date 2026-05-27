using Bunit;
using Microsoft.Extensions.DependencyInjection;
using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Services;
using OrderTrackingBlazor.Components.Pages;
using TestContext = Bunit.TestContext;

namespace OrderTracking.Blazor.Tests;

[TestClass]
public class HomeTests
{
    private static Parcel CreateParcel(string trackingId, bool delivered = false)
    {
        return new Parcel
        {
            TrackingId = trackingId,
            Company = trackingId[..2].ToUpperInvariant(),
            StatusDescription = "In transit",
            DeliveredAt = delivered ? DateTimeOffset.Now : null,
            Events =
            [
                new ParcelEvent
                {
                    Description = "Created",
                    Location = "Helsinki",
                    Timestamp = DateTimeOffset.Now
                }
            ]
        };
    }

    private static IRenderedComponent<Home> RenderHomeWithState(TestContext ctx, ParcelState state)
    {
        ctx.Services.AddScoped(_ => state);
        return ctx.RenderComponent<Home>();
    }

    [TestMethod]
    public void Home_ComponentRendered_RendersInputAndAddButton()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);

        Assert.IsNotNull(cut.Find("input.ourtxt"));
        Assert.IsNotNull(cut.Find("button.ourbtn"));
    }

    [TestMethod]
    public void Home_EmptyInputProvided_ShowsErrorNotification()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change(string.Empty);
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "ID cannot be null or empty");
        });
    }

    [TestMethod]
    public void Home_WhitespaceInputProvided_ShowsErrorNotification()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("   ");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "ID cannot be null or empty");
        });
    }

    [TestMethod]
    public void Home_SpecialCharacterInputProvided_ShowsSpecialCharacterErrorNotification()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("MH321!!!49312FI");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "Tracking number cannot contain special characters");
        });
    }

    [TestMethod]
    public void Home_DigitsOnlyInputProvided_ShowsInvalidTrackingNumberError()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("31804731");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "Invalid tracking number");
        });
    }

    [TestMethod]
    public void Home_InputWithOnlyLetters_ShowsInvalidTrackingNumber()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("MHFI");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "Invalid tracking number");
        });
    }

    [TestMethod]
    public void Home_InputWithInternalWhitespace_ShowsSpecialCharacterError()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("MH 302164795 FI");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "Tracking number cannot contain special characters");
        });
    }

    [TestMethod]
    public void Home_DuplicateInputInActiveList_ShowsAlreadyInUseError()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();
        state.ActiveParcels.Add(CreateParcel("MH302164795FI"));

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("mh302164795fi");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "The tracking ID is already in use");
        });
    }

    [TestMethod]
    public void Home_DuplicateInputInDeliveredList_ShowsAlreadyInUseError()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();
        state.DeliveredParcels.Add(CreateParcel("JJ123456789FI", delivered: true));

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("jj123456789fi");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "The tracking ID is already in use");
        });
    }

    [TestMethod]
    public void Home_DuplicateInputWithLeadingAndTrailingWhitespace_ShowsAlreadyInUseError()
    {
        using var ctx = new TestContext();
        var state = new ParcelState();
        state.ActiveParcels.Add(CreateParcel("MH302164795FI"));

        var cut = RenderHomeWithState(ctx, state);
        cut.Find("input.ourtxt").Change("   mh302164795fi   ");
        cut.Find("button.ourbtn").Click();

        cut.WaitForAssertion(() =>
        {
            StringAssert.Contains(cut.Markup, "Add Exception:");
            StringAssert.Contains(cut.Markup, "The tracking ID is already in use");
        });
    }
}