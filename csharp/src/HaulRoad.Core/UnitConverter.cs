namespace HaulRoad.Core;

public static class UnitConverter
{
    private const double TonnesPerShortTon = 0.90718474;
    private const double KilometresPerMile = 1.609344;
    private const double LitresPerUsGallon = 3.785411784;

    public static double ToTonnes(double value, PayloadUnit unit)
    {
        RequireNonNegative(value);
        return unit == PayloadUnit.ShortTons ? value * TonnesPerShortTon : value;
    }

    /// <summary>Speed keeps its sign: a negative value means the truck is reversing.</summary>
    public static double ToKilometresPerHour(double value, SpeedUnit unit)
    {
        RequireFinite(value);
        return unit == SpeedUnit.MilesPerHour ? value * KilometresPerMile : value;
    }

    public static double ToLitres(double value, FuelUnit unit)
    {
        RequireNonNegative(value);
        return unit == FuelUnit.UsGallons ? value * LitresPerUsGallon : value;
    }

    private static void RequireFinite(double value)
    {
        if (!double.IsFinite(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Reading must be a finite number.");
        }
    }

    private static void RequireNonNegative(double value)
    {
        RequireFinite(value);

        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Reading cannot be negative.");
        }
    }
}
