namespace HaulRoad.Core;

public static class LoadStateClassifier
{
    // Business rules from Field Operations: share of rated capacity.
    private const double EmptyBelowRatio = 0.10;
    private const double OverloadedAboveRatio = 1.05;

    public static LoadState Classify(double payloadTonnes, double ratedCapacityTonnes)
    {
        RequireValid(payloadTonnes, nameof(payloadTonnes), allowZero: true);
        RequireValid(ratedCapacityTonnes, nameof(ratedCapacityTonnes), allowZero: false);

        var ratio = payloadTonnes / ratedCapacityTonnes;

        if (ratio < EmptyBelowRatio)
        {
            return LoadState.Empty;
        }

        return ratio <= OverloadedAboveRatio ? LoadState.Loaded : LoadState.Overloaded;
    }

    private static void RequireValid(double value, string parameterName, bool allowZero)
    {
        var belowMinimum = allowZero ? value < 0 : value <= 0;

        if (!double.IsFinite(value) || belowMinimum)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, "Value is not valid for this reading.");
        }
    }
}
