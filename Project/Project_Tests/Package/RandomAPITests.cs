using OrderTracking.Core.Models.Mapping.API;
using OrderTracking.Core.Models.Mapping.DTO;

namespace Project_Tests;

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
