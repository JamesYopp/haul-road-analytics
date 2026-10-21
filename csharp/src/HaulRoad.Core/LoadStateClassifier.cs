namespace HaulRoad.Core;

public static class LoadStateClassifier
{
    public static LoadState Classify(double payloadTonnes, double ratedCapacityTonnes)
    {
        if (!double.IsFinite(payloadTonnes) || payloadTonnes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(payloadTonnes), payloadTonnes, "Payload must be a finite, non-negative number.");
        }

        if (!double.IsFinite(ratedCapacityTonnes) || ratedCapacityTonnes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ratedCapacityTonnes), ratedCapacityTonnes, "Rated capacity must be a finite, positive number.");
        }

        var ratio = payloadTonnes / ratedCapacityTonnes;

        if (ratio < 0.10)
        {
            return LoadState.Empty;
        }

        if (ratio <= 1.05)
        {
            return LoadState.Loaded;
        }

        return LoadState.Overloaded;
    }
}
