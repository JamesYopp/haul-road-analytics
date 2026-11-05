# Feature PRD: Summarize stored payload readings

Input to `create-epics-stories` for Block 2. Treat the Field Operations lead as your customer proxy; the open questions at the end are for them.

## Problem
Trucks send a payload reading every few seconds. The readings are stored exactly as the truck sent them. Mine planners want to know how heavily a truck was loaded over a period, for example one shift. Some readings are "data not available", and if those are averaged in, the answer is wrong without looking wrong.

## Goals
- A planner can ask for a truck's payload summary over a time range.
- Readings with no data never distort the average.
- The summary says how many readings it used and how many it set aside.

## In scope
- A summary service: average payload in tonnes, count of usable readings, count of unavailable readings, for one truck over a time range.
- Storing and fetching readings through the reading repository (`IReadingRepository`, already in the code).
- A SQLite implementation of the repository (`SqliteReadingRepository`), for practice, in its own project (`HaulRoad.Data`). The real system uses a different database, so keep every SQLite detail inside that class and that project. The domain project (`HaulRoad.Core`) must not reference SQLite.

## Out of scope
Ingesting from the truck, route segmentation, fuel burn, any UI, more than one truck per query.

## Business rules (from Field Operations)
1. A reading is stored as received: truck id, UTC timestamp, and a raw 16-bit payload in whole tonnes.
2. A raw payload with every bit set (`0xFFFF`, 65535) means "data not available". It does not mean 65535 tonnes. This is the CAN bus convention.
3. The summary averages only readings that have data.
4. The summary counts readings used and readings set aside.
5. Summary logic must be testable without a database. Storage sits behind `IReadingRepository`.

## Interface to use
So that every pair's tests line up, build the summary service with this shape. You create `PayloadSummary` and `PayloadStatsService` in `HaulRoad.Core`. What `Summarize` does with the readings is settled by the business rules and your customer proxy.

```csharp
public sealed class PayloadStatsService
{
    public PayloadStatsService(IReadingRepository readings) { ... }
    public PayloadSummary Summarize(string truckId, DateTimeOffset from, DateTimeOffset to) { ... }
}

public sealed record PayloadSummary(double? AverageTonnes, int UsedCount, int UnavailableCount);
```

## Success
A planner can trust the average without checking whether unavailable readings leaked in, and the team can test the summary logic in milliseconds.

## Open questions
Resolve these with your customer proxy before you plan. Do not guess.
- What should the summary report when a truck has no readings in the range, or none with data?
- Is the end of a time range included or excluded? What about the start?
- In what order should readings come back?
- What should the repository do with a reading it cannot store?
