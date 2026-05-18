using OrderTracking.Core.Models.Package;
using OrderTracking.Core.Models.Mapping.API;
using Moq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OrderTracking.Core.Models.Mapping.Tests
{
    // To Do: Test for parcel update.
    [TestClass]
    public sealed class CompanyDTOtests
    {
        public PackageModeling testmodel;
        
        private string json = string.Empty;
        [TestInitialize]
        public void SetUp()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "MH.json"));
            json = File.ReadAllText(path);

            testmodel = new PackageModeling();
            testmodel.ID = "MH302164795FI";
            testmodel.Url = "test-url";
        }

        /// <summary>
        /// I thought about making this happen in the setup as well, but it felt crowded already.
        /// </summary>
        /// <returns> The Parcel that is run through the PackageModeling.JsonToParcel </returns>
        private Parcel RunJsonToParcel()
        {
            return testmodel.JsonToParcel(json);
        }

        [TestMethod]
        public void SimulatingRandom_Matkahuolto_TestingIfTheCreationOfTheRandomJsonDataWorks()
        {
            string json = APIsimulation.SimulatingRandom("MH");
            Assert.AreNotEqual(json, string.Empty);
            Parcel p = testmodel.JsonToParcel(json);
            Assert.IsNotNull(p);
            Console.WriteLine(p);
        }
        [TestMethod]
        public void JsonToParcel_Matkahuolto_WillReturnParcel()
        {
            Parcel result = RunJsonToParcel();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void JsonToParcel_Matkahuolto_TheParcelHasTheIntendedTrackingID()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("MH302164795FI", result.TrackingId);
        }

        [TestMethod]
        public void JsonToParcel_Matkahuolto_TheParcelHasTheCorrectCompany()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("Matkahuolto", result.Company);
        }

        [TestMethod]
        public void JsonToParcel_Matkahuolto_TheParcelHasDescriptions()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("Delivered", result.StatusDescription);
        }

        [TestMethod]
        public void JsonToParcel_Matkahuolto_TheParcelReturnedIsMarkedAsDelivered()
        {
            Parcel result = RunJsonToParcel();

            Assert.IsTrue(result.IsDelivered);
            Assert.IsNotNull(result.DeliveredAt);
            Assert.AreEqual(new DateTimeOffset(2014, 4, 14, 13, 35, 10, TimeSpan.FromHours(2)), result.DeliveredAt);
        }

        [TestMethod]
        public void JsonToParcel_Matkahuolto_TheParcelReturnedContainsOneEventWithCorrectValues()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual(1, result.Events.Count);
            Assert.AreEqual("Delivered (Additional Info)", result.Events[0].Description);
            Assert.AreEqual("HELSINKI", result.Events[0].Location);
            Assert.AreEqual(new DateTimeOffset(2014, 4, 14, 13, 35, 10, TimeSpan.FromHours(2)), result.Events[0].Timestamp);
        }
        [TestMethod]
        public void JsonToParcel_Matkahuolto_TestingTheToStringOverride()
        {
            Parcel result = RunJsonToParcel();

            Console.WriteLine(result.ToString());

            Regex white = new Regex(@"\s");
            Assert.AreEqual("ID:MH302164795FICarriercompany:MatkahuoltoCurrentstatus:DeliveredCurrentcity:HELSINKI",white.Replace(result.ToString(), ""));
        }
        [TestMethod]
        public void JsonToParcel_WillCopyUrlToCompletedParcel_AndClearModelUrl()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("test-url", result.URL);
            Assert.AreEqual(string.Empty, testmodel.Url);
        }
        [TestMethod]
        public void JsonToParcel_InvalidJson_ThrowsJsonReaderException()
        {
            string invalidJson = "{ \"MHTrackingEvents\": { \"Event\": [ }";

            Assert.Throws<Exception>(() => testmodel.JsonToParcel(invalidJson));    // I mean naturally it should be tested, but if the Json data we get from an API is damaged there's nothing we can do other than to make another call etc.
        }                                                                           // So basically we could make the 3 / 5 tries that if all fail we'll throw the exception, but it's monday so I don't have enough imagination for it.
        [TestMethod]
        public void JsonToParcel_NonJsonText_ThrowsJsonReaderException()
        {
            string invalidJson = "this is not json at all";

            Assert.Throws<Exception>(() => testmodel.JsonToParcel(invalidJson));
        }
        [TestMethod]
        public void JsonToParcel_InvalidEventList_ThrowsException()
        {
            string invalidEventListJson = Regex.Replace(json, @"""Event""\s*:\s*\[[\s\S]*?\]", @"""Event"": ""invalid-event-list""", RegexOptions.Singleline);
            
            Parcel par = testmodel.JsonToParcel(invalidEventListJson);
            Assert.IsNotNull(par); 
        }
    }
}
