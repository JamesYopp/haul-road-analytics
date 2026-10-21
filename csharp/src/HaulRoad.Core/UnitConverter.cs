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

    public static double ToKilometresPerHour(double value, SpeedUnit unit)
    {
        throw new NotImplementedException();
    }

    public static double ToLitres(double value, FuelUnit unit)
    {
        throw new NotImplementedException();
    }
}
