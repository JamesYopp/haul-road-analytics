using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class LoadStateClassifierTests
{
    // Rated capacity 220 tonnes: 10% = 22.0, 105% = 231.0
    [Theory]
    [InlineData(0.0, LoadState.Empty)]
    [InlineData(21.99, LoadState.Empty)]
    [InlineData(22.0, LoadState.Loaded)]
    [InlineData(110.0, LoadState.Loaded)]
    [InlineData(231.0, LoadState.Loaded)]
    [InlineData(231.01, LoadState.Overloaded)]
    [InlineData(250.0, LoadState.Overloaded)]
    public void Classify_UsesShareOfRatedCapacity(double payloadTonnes, LoadState expected)
    {
        var result = LoadStateClassifier.Classify(payloadTonnes, 220.0);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1.0, 220.0)]
    [InlineData(double.NaN, 220.0)]
    [InlineData(100.0, 0.0)]
    [InlineData(100.0, -220.0)]
    [InlineData(100.0, double.NaN)]
    public void Classify_Throws_WhenInputsAreInvalid(double payloadTonnes, double ratedCapacityTonnes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => LoadStateClassifier.Classify(payloadTonnes, ratedCapacityTonnes));
    }
}
