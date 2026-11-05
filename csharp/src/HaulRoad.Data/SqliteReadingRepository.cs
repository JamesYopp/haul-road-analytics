using HaulRoad.Core;
using Microsoft.Data.Sqlite;

namespace HaulRoad.Data;

public sealed class SqliteReadingRepository : IReadingRepository
{
    public SqliteReadingRepository(SqliteConnection connection)
    {
        _ = connection;
    }

    public void Add(TelemetryReading reading)
    {
    }

    public IReadOnlyList<TelemetryReading> GetBetween(string truckId, DateTimeOffset from, DateTimeOffset to)
    {
        return new List<TelemetryReading>();
    }
}
