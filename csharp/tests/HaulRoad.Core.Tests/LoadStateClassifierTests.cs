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
}
