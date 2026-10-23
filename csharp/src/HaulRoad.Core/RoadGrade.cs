namespace HaulRoad.Core;

public static class RoadGrade
{
    public static double GradePercent(double rise, double run)
    {
        return rise / run * 100.0;
    }
}
