# Block 2 pair lab guide

**Time:** 50 minutes for the lab, then 20 minutes for the skill upgrade. **Format:** pairs.

## Setup

Open the practice repo (`haul-road-analytics`) in VS Code on a new branch from the Block 2 start tag:

```
git fetch --tags
git switch -c block2-work block2-start
dotnet test csharp/HaulRoad.sln
```

All tests should pass (Block 1 plus the decoder). The solution now has four projects: `HaulRoad.Core` (domain), `HaulRoad.Data` (database adapters), `HaulRoad.Core.Tests` (unit tests) and `HaulRoad.Data.IntegrationTests` (integration tests). Your team skills are in `.agents` inside the repo. For the skill upgrade, create a branch inside `.agents` named `block2/<your-pair>`.

## Five rules

The first three are from Block 1.

1. No failing test, no production code.
2. See it fail for the right reason: a failing assertion, not a compile error.
3. Commit red before green.
4. A unit test must not touch the real clock, network, file system, database, or randomness. Unit tests live in `HaulRoad.Core.Tests`.
5. An integration test covers one adapter, uses a real in-memory dependency, and lives in `HaulRoad.Data.IntegrationTests`.

## What you are given

Read these first, they are short: `prd/block2-feature.md`, and in `csharp/src/HaulRoad.Core` the types `IReadingRepository`, `TelemetryReading` and `PayloadDecoder`. `ReadingSchema` is in `csharp/src/HaulRoad.Data`.

## Steps

### 1. Ask your customer proxy (5 minutes)
Read `prd/block2-feature.md`. The open questions at the end are real. The Field Operations lead (your customer proxy) takes questions from the whole group: ask yours there and write down the answers. Do not guess.

### 2. Stories and plan (10 minutes)
Run `create-epics-stories`, then `plan-story` on the PRD. Check the output:
- Can each AC be turned into one automated test, with concrete values?
- Does the plan say, for every test, whether it is a unit test or an integration test, and which doubles it needs?
- Does the plan keep storage details out of the summary logic?

Log every shortfall. If `plan-story` refuses a test that needs a database, read its constraint again: where does that rule still apply, and where does it not?

### 3. Part A: the summary logic, with a fake (15 minutes)
Build `PayloadStatsService` (in `HaulRoad.Core`) test-first. It takes an `IReadingRepository` in its constructor and reads from it. It never touches a database. Use this shape, so everyone's tests line up:

```csharp
public sealed class PayloadStatsService
{
    public PayloadStatsService(IReadingRepository readings) { ... }
    public PayloadSummary Summarize(string truckId, DateTimeOffset from, DateTimeOffset to) { ... }
}

public sealed record PayloadSummary(double? AverageTonnes, int UsedCount, int UnavailableCount);
```

`IReadingRepository` and `TelemetryReading` already exist in `HaulRoad.Core`; read them first. You create `PayloadSummary` and the service. What `Summarize` should do with the readings is for the PRD and your customer proxy to settle. Write a small hand-made fake repository in the unit test project (`HaulRoad.Core.Tests`) and use it in your tests. Run them with `dotnet test csharp/tests/HaulRoad.Core.Tests`. Ping-pong: one partner writes the next failing test, the other makes it pass, then swap. Run `dotnet test` after every step and commit red, then green. Refactor when green.

Stretch: write one test with NSubstitute that stubs the repository. Assert on the outcome, not on which methods were called.

### 4. Part B: the real adapter, in memory (20 minutes)
Checkpoint if you are behind: `git switch -c block2-work-2 block2-db-ready` gives you a working Part A and a database smoke test.

1. In `HaulRoad.Data.IntegrationTests`, write a smoke test: an in-memory SQLite database has the `readings` table (use `ReadingSchema.Create`). See it pass.
2. Build `SqliteReadingRepository` in `HaulRoad.Data` test-first, one behavior at a time. Everything SQLite-specific stays in that project, and `HaulRoad.Core` must not reference it. Start with the behaviors you think matter most: a stored reading comes back, only the requested truck's readings, the edges of a time range, order, and the raw payload unchanged.
3. Run only the integration tests with `dotnet test csharp/tests/HaulRoad.Data.IntegrationTests`. Run everything with `dotnet test csharp/HaulRoad.sln`.

### 5. Classify and log (throughout)
Keep a table of every test you wrote: unit or integration, which project it lives in, and why (format at the end of this guide). Add shortfalls to your shortfall log as you go.

## Skill upgrade (20 minutes)

1. Run `review-implementation` on your work. What did it flag? What should it have flagged and did not? Look especially at: tests that use real time or I/O but call themselves unit tests, direct use of `DateTime.UtcNow`, tests that only verify a mock was called, an adapter with no integration test, and tests that were never seen red, and tests in the wrong project.
2. For each shortfall, write a criterion you can answer yes or no, or count. Write them before you change any skill.
3. Edit the skill's Context (why) and Constraints (how) in your branch of the skills repo. Every instruction must be load-bearing.
4. Re-run the review on the same input. Score the new output against your criteria.
5. Cut any instruction no criterion would miss.
6. Open a pull request using the format at the end of this guide. Include before and after scores. If you also changed `plan-story`, say what its rule now covers.

## Hand in

- Your branch in the practice repo with red and green commits
- Your test classification table and shortfall log
- A pull request to the team DLP Skills repo

## Stuck?

- Environment: `git switch -c block2-work-2 block2-start`
- Part A done, Part B not started: `git switch -c block2-work-2 block2-db-ready` gives you a working Part A and a database smoke test.
- See how the reference solution was built: `git log --oneline block2-start..block2-solution`
- A compile error about `Sqlite` in the unit test project? The adapter and its tests belong in `HaulRoad.Data` and `HaulRoad.Data.IntegrationTests`.
- "no such table: readings": an in-memory SQLite database lives only as long as the connection that opened it. Open one connection, create the schema on it, and keep it open for the whole test.
- Ask the facilitator or your partner before you ask the AI to fix it for you.

## Appendix: formats

### Test classification

| Test | Unit or integration | Lives in | Why |
| --- | --- | --- | --- |
|  |  |  |  |

### Shortfall log

| Skill | What I expected | What I got | Candidate criterion (yes/no or countable) |
| --- | --- | --- | --- |
|  |  |  |  |

### Pull request body for the skills repo

```
## Skill changed

## Shortfall observed

## Criteria (written before the change)
1.
2.

## Scores
Before: _ of _ criteria, _ items checked
After:  _ of _ criteria, _ items checked

## Load-bearing audit

## Pair
```
