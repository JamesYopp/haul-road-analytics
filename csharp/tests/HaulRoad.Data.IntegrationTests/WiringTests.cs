using HaulRoad.Data;
using Xunit;

namespace HaulRoad.Data.IntegrationTests;

public class WiringTests
{
    [Fact]
    public void IntegrationTestProject_CanReferenceDataAssembly()
    {
        Assert.Equal("HaulRoad.Data", typeof(ReadingSchema).Assembly.GetName().Name);
    }
}
