using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class UnitConverterTests
{
    [Fact]
    public void ToTonnes_ReturnsSameValue_WhenUnitIsTonnes()
    {
        var result = UnitConverter.ToTonnes(100.0, PayloadUnit.Tonnes);

        Assert.Equal(100.0, result);
    }
}
