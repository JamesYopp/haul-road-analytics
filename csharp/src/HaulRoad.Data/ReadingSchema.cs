using Microsoft.Data.Sqlite;

namespace HaulRoad.Data;

/// <summary>Creates the table that holds readings. Practice database only (SQLite). No index, on purpose.</summary>
public static class ReadingSchema
{
    public static void Create(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE readings (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                truck_id TEXT NOT NULL,
                recorded_at_utc INTEGER NOT NULL,
                raw_payload INTEGER NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }
}
