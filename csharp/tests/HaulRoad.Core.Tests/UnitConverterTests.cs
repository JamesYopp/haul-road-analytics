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

    [Theory]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    public void ToTonnes_Throws_WhenReadingIsInvalid(double invalidValue)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => UnitConverter.ToTonnes(invalidValue, PayloadUnit.Tonnes));
    }

    [Theory]
    [InlineData(60.0, SpeedUnit.MilesPerHour, 96.56064)]
    [InlineData(60.0, SpeedUnit.KilometresPerHour, 60.0)]
    [InlineData(-10.0, SpeedUnit.MilesPerHour, -16.09344)]
    public void ToKilometresPerHour_ConvertsAndKeepsSign(double value, SpeedUnit unit, double expected)
    {
        var result = UnitConverter.ToKilometresPerHour(value, unit);

        Assert.Equal(expected, result, precision: 6);
    }

    [Fact]
    public void ToKilometresPerHour_Throws_WhenSpeedIsNotANumber()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => UnitConverter.ToKilometresPerHour(double.NaN, SpeedUnit.MilesPerHour));
    }

    [Theory]
    [InlineData(10.0, FuelUnit.UsGallons, 37.85411784)]
    [InlineData(10.0, FuelUnit.Litres, 10.0)]
    public void ToLitres_Converts(double value, FuelUnit unit, double expected)
    {
        var result = UnitConverter.ToLitres(value, unit);

        Assert.Equal(expected, result, precision: 6);
    }

    [Fact]
    public void ToLitres_Throws_WhenFuelIsNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => UnitConverter.ToLitres(-0.5, FuelUnit.Litres));
    }
}
