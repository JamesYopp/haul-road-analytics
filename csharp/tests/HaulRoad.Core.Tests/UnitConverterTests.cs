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

    [Theory]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 0.90718474)]
    [InlineData(100.0, 90.718474)]
    public void ToTonnes_ConvertsShortTonsToTonnes(double shortTons, double expectedTonnes)
    {
        var result = UnitConverter.ToTonnes(shortTons, PayloadUnit.ShortTons);

        Assert.Equal(expectedTonnes, result, precision: 6);
    }
}
