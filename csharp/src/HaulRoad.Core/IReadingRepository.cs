namespace HaulRoad.Core;

/// <summary>Where telemetry readings are stored. Analysis code depends on this, never on a database.</summary>
public interface IReadingRepository
{
    void Add(TelemetryReading reading);

    IReadOnlyList<TelemetryReading> GetBetween(string truckId, DateTimeOffset from, DateTimeOffset to);
}
