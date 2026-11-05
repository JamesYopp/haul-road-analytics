namespace HaulRoad.Core;

/// <summary>
/// One payload reading exactly as the truck sent it. RawPayload is the 16-bit CAN field,
/// in whole tonnes. All 16 bits set (0xFFFF) is the CAN convention for "data not available".
/// </summary>
public sealed record TelemetryReading(string TruckId, DateTimeOffset Timestamp, ushort RawPayload);
