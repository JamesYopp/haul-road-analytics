using HaulRoad.Core;
using Microsoft.Data.Sqlite;

namespace HaulRoad.Data;

/// <summary>
/// SQLite implementation of the reading repository. Every SQLite detail lives here: the SQL text,
/// parameter names, and storing timestamps as unix milliseconds in an INTEGER column.
/// </summary>
public sealed class SqliteReadingRepository : IReadingRepository
{
    private const string InsertSql =
        "INSERT INTO readings (truck_id, recorded_at_utc, raw_payload) VALUES ($truck, $at, $raw)";

    // Start inclusive, end exclusive, oldest first (insert order breaks ties).
    private const string SelectRangeSql =
        "SELECT truck_id, recorded_at_utc, raw_payload FROM readings " +
        "WHERE truck_id = $truck AND recorded_at_utc >= $from AND recorded_at_utc < $to " +
        "ORDER BY recorded_at_utc, id";

    private readonly SqliteConnection _connection;

    public SqliteReadingRepository(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void Add(TelemetryReading reading)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = InsertSql;
        command.Parameters.AddWithValue("$truck", reading.TruckId);
        command.Parameters.AddWithValue("$at", ToStored(reading.Timestamp));
        command.Parameters.AddWithValue("$raw", (int)reading.RawPayload);
        command.ExecuteNonQuery();
    }

    public IReadOnlyList<TelemetryReading> GetBetween(string truckId, DateTimeOffset from, DateTimeOffset to)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = SelectRangeSql;
        command.Parameters.AddWithValue("$truck", truckId);
        command.Parameters.AddWithValue("$from", ToStored(from));
        command.Parameters.AddWithValue("$to", ToStored(to));

        using var reader = command.ExecuteReader();
        var results = new List<TelemetryReading>();

        while (reader.Read())
        {
            results.Add(new TelemetryReading(
                reader.GetString(0),
                FromStored(reader.GetInt64(1)),
                (ushort)reader.GetInt32(2)));
        }

        return results;
    }

    private static long ToStored(DateTimeOffset timestamp) => timestamp.ToUnixTimeMilliseconds();

    private static DateTimeOffset FromStored(long unixMilliseconds) =>
        DateTimeOffset.FromUnixTimeMilliseconds(unixMilliseconds);
}
