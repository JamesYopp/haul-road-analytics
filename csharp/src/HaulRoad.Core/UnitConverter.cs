namespace HaulRoad.Core;

public static class UnitConverter
{
    public static double ToTonnes(double value, PayloadUnit unit)
    {
        if (!double.IsFinite(value) || value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Reading must be a finite, non-negative number.");
        }

        return unit == PayloadUnit.ShortTons ? value * 0.90718474 : value;
    }
}
