using LähetysSeurantaConsole.Model.Package;
using LähetysSeurantaConsole.Model.Package.API;
using Moq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Project_Tests.Package
{
    [TestClass]
    public sealed class CompanyDTOtests
    {
        public PackageModeling testmodel;
        public Mock<IPackage> interfacemock;

        private string json = string.Empty;
        [TestInitialize]
        public void SetUp()
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "jsontest.json"));
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
        public void APIsimulation_Matkahuolto_SeeingIfTheSimulationWorks()
        {
            string json = APIsimulation.SimulationFromTheXML(testmodel.ID);
            Parcel c = testmodel.JsonToParcel(json);
            Assert.IsNotNull(c);
            Console.WriteLine(c);
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
        public void JsonToParcel_Matkahuolto_TheParcelHasTheCorrectRecipientName()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("Mr. Receiver", result.RecipientName);
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
            Assert.AreEqual("ID:MH302164795FICarriercompany:MatkahuoltoCurrentstatus:Delivered",white.Replace(result.ToString(), ""));
        }
        [TestMethod]
        public void JsonToParcel_WillCopyUrlToCompletedParcel_AndClearModelUrl()
        {
            Parcel result = RunJsonToParcel();

            Assert.AreEqual("test-url", result.URL);
            Assert.AreEqual(string.Empty, testmodel.Url);
        }
    }
}
