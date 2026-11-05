using HaulRoad.Core;

namespace HaulRoad.Core.Tests;

/// <summary>A hand-written test double: readings live in a list, no database.</summary>
internal sealed class FakeReadingRepository : IReadingRepository
{
    private readonly List<TelemetryReading> _readings = new();

    public void Add(TelemetryReading reading) => _readings.Add(reading);

    public IReadOnlyList<TelemetryReading> GetBetween(string truckId, DateTimeOffset from, DateTimeOffset to) =>
        _readings
            .Where(r => r.TruckId == truckId && r.Timestamp >= from && r.Timestamp < to)
            .OrderBy(r => r.Timestamp)
            .ToList();
}
