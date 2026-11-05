namespace HaulRoad.Core;

public sealed class PayloadStatsService
{
    private readonly IReadingRepository _readings;

    public PayloadStatsService(IReadingRepository readings)
    {
        _readings = readings;
    }

    public PayloadSummary Summarize(string truckId, DateTimeOffset from, DateTimeOffset to)
    {
        var readings = _readings.GetBetween(truckId, from, to);
        var usable = new List<double>();
        var unavailable = 0;

        foreach (var reading in readings)
        {
            if (PayloadDecoder.TryDecodeTonnes(reading.RawPayload, out var tonnes))
            {
                usable.Add(tonnes);
            }
            else
            {
                unavailable++;
            }
        }

        double? average = usable.Count == 0 ? null : usable.Average();

        return new PayloadSummary(average, usable.Count, unavailable);
    }
}
