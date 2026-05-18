using OrderTracking.Core.Models.Mapping.DTO;
using OrderTracking.Core.Models.Package;
using System.Text.Json;
using Newtonsoft.Json;

namespace Project_Tests;


[TestClass]
public class PostiDTOTests
{
    private string json = string.Empty;
    [TestInitialize]

    public void SetUp()
    {
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Posti.json"));
        json = File.ReadAllText(path);
    }

    [TestMethod]
    public void PostiDTO_ToParcel_ParcelCorrectly()
    {
        Parcel p = PostiDTO.ToParcel(json);

        Assert.IsNotNull(p);
        Console.WriteLine(p.ToString());
    }
    [TestMethod]
    public void PostiDTO_ToParcel_ParcelHasTheIntendedTrackingID()
    {
        Parcel p = PostiDTO.ToParcel(json);

        Assert.AreEqual("JJFI00000000000000", p.TrackingId);
    }
    [TestMethod]
    public void PostiDTO_ToParcel_ParcelHasTheIntendedDeliveredAt()
    {
        Parcel p = PostiDTO.ToParcel(json);
        Assert.AreEqual(DateTimeOffset.Parse("2024-05-12T10:00:00+03:00"), p.DeliveredAt);
    }
}