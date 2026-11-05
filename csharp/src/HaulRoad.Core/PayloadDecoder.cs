namespace HaulRoad.Core;

public static class PayloadDecoder
{
    // CAN bus convention: "data not available" is every bit set in the field. For 16 bits that is 0xFFFF.
    public const ushort NotAvailable = ushort.MaxValue;

    public static bool TryDecodeTonnes(ushort rawPayload, out double tonnes)
    {
        if (rawPayload == NotAvailable)
        {
            tonnes = 0;
            return false;
        }

        tonnes = rawPayload;
        return true;
    }

    public static LoadState Classify(ushort rawPayload, double ratedCapacityTonnes)
    {
        return TryDecodeTonnes(rawPayload, out var tonnes)
            ? LoadStateClassifier.Classify(tonnes, ratedCapacityTonnes)
            : LoadState.Unknown;
    }
}
