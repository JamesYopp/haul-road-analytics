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

        return new PayloadSummary(readings.Average(r => (double)r.RawPayload), readings.Count, 0);
    }
}
