using HaulRoad.Core;
using Microsoft.Data.Sqlite;

namespace HaulRoad.Data;

public sealed class SqliteReadingRepository : IReadingRepository
{
    private readonly SqliteConnection _connection;

    public SqliteReadingRepository(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void Add(TelemetryReading reading)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            "INSERT INTO readings (truck_id, recorded_at_utc, raw_payload) VALUES ($truck, $at, $raw)";
        command.Parameters.AddWithValue("$truck", reading.TruckId);
        command.Parameters.AddWithValue("$at", reading.Timestamp.ToUnixTimeMilliseconds());
        command.Parameters.AddWithValue("$raw", (int)reading.RawPayload);
        command.ExecuteNonQuery();
    }

    public IReadOnlyList<TelemetryReading> GetBetween(string truckId, DateTimeOffset from, DateTimeOffset to)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            "SELECT truck_id, recorded_at_utc, raw_payload FROM readings " +
            "WHERE truck_id = $truck AND recorded_at_utc >= $from AND recorded_at_utc < $to";
        command.Parameters.AddWithValue("$truck", truckId);
        command.Parameters.AddWithValue("$from", from.ToUnixTimeMilliseconds());
        command.Parameters.AddWithValue("$to", to.ToUnixTimeMilliseconds());

        using var reader = command.ExecuteReader();
        var results = new List<TelemetryReading>();

        while (reader.Read())
        {
            results.Add(new TelemetryReading(
                reader.GetString(0),
                DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(1)),
                (ushort)reader.GetInt32(2)));
        }

        return results;
    }
}
