using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class PayloadDecoderTests
{
    [Theory]
    [InlineData((ushort)0, 0.0)]
    [InlineData((ushort)220, 220.0)]
    [InlineData((ushort)0xFFFE, 65534.0)]
    public void TryDecodeTonnes_ReturnsTheValue_WhenAvailable(ushort raw, double expected)
    {
        var available = PayloadDecoder.TryDecodeTonnes(raw, out var tonnes);

        Assert.True(available);
        Assert.Equal(expected, tonnes);
    }

    [Fact]
    public void TryDecodeTonnes_ReportsNotAvailable_WhenAllBitsAreSet()
    {
        var available = PayloadDecoder.TryDecodeTonnes(0xFFFF, out _);

        Assert.False(available);
    }

    // Rated capacity 220 tonnes: Empty below 22, Overloaded above 231.
    [Theory]
    [InlineData((ushort)21, LoadState.Empty)]
    [InlineData((ushort)110, LoadState.Loaded)]
    [InlineData((ushort)232, LoadState.Overloaded)]
    [InlineData((ushort)0xFFFE, LoadState.Overloaded)]
    [InlineData((ushort)0xFFFF, LoadState.Unknown)]
    public void Classify_UsesTheDecodedPayload_AndMarksUnavailableAsUnknown(ushort raw, LoadState expected)
    {
        var result = PayloadDecoder.Classify(raw, 220.0);

        Assert.Equal(expected, result);
    }
}
