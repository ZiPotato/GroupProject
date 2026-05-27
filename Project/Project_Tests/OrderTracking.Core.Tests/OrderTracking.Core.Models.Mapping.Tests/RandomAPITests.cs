using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Mapping.DTO;

namespace OrderTracking.Core.Models.Mapping.Tests;

[TestClass]
public class RandomAPITests
{
    [TestMethod]
    public void PostiAPISimulation_TestIsNotNull_ReturnsTrue()
    {
        string testId = "JJFI123456789";

        string result = PostiAPISimulation.SimulatingRandomPosti(testId);
        
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);
    }
}
