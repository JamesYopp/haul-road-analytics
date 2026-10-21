namespace HaulRoad.Core;

public static class UnitConverter
{
    public static double ToTonnes(double value, PayloadUnit unit)
    {
        return unit == PayloadUnit.ShortTons ? value * 0.90718474 : value;
    }
}
