namespace HaulRoad.Core;

public static class RoadGrade
{
    public static double GradePercent(double rise, double run)
    {
        if (!double.IsFinite(run) || run <= 0)
            throw new ArgumentOutOfRangeException(nameof(run), run, "Run must be a positive, finite distance.");

        return rise / run * 100.0;
    }
}
