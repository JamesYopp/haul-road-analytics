namespace HaulRoad.Core;

public static class LoadStateClassifier
{
    public static LoadState Classify(double payloadTonnes, double ratedCapacityTonnes)
    {
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
