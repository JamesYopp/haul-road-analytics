using HaulRoad.Core;
using Xunit;

namespace HaulRoad.Core.Tests;

public class RoadGradeTests
{
    [Fact]
    public void GradePercent_ReturnsRiseOverRunAsPercent()
    {
        var result = RoadGrade.GradePercent(5.0, 100.0);

        Assert.Equal(5.0, result, precision: 6);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-100.0)]
    public void GradePercent_Throws_WhenRunIsNotPositive(double run)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RoadGrade.GradePercent(5.0, run));
    }
}
