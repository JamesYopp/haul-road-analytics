using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class SmokeTests
{
    [Fact]
    public void TestProject_CanReferenceCoreAssembly()
    {
        Assert.Equal("HaulRoad.Core", typeof(AssemblyMarker).Assembly.GetName().Name);
    }
}
