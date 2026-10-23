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
}
