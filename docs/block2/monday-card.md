# Block 2 Monday card

**This week, on one real ticket, before you write the test, ask:**

1. What in this code can fail for a reason that is not in my code? (clock, database, network, file, randomness, another service)
2. Is it behind something I can replace? If not, cut the seam first.
3. Does this test need the real thing, or a stand-in?

**Which double?** Fake (a small working version) first. Stub (canned answers) next. Mock (checks calls) last, and only when the call is the behavior.

**Unit or integration?** Unit: no clock, network, file system, database, or randomness, and it runs in milliseconds. Integration: one real adapter, in memory or local, in its own test project. If a "unit" test can fail because of something outside your code, it is an integration test in disguise.

**When AI writes your tests, check:**
- Does it call the real clock, database, or network?
- Is the only assertion that a mock was called?
- Did I see it fail before the code existed?
- Does each adapter have at least one real integration test, in the integration project?

**In code review, ask:** is this a unit test, and how do you know?

**Bring to your embedded session:** a ticket, one place you cut a seam, your test classification table, your shortfall log.

**Language:** C#: `TimeProvider` and `FakeTimeProvider`, NSubstitute, in-memory SQLite. Python: pass the clock or the repository in; fakes and `monkeypatch` in pytest. JavaScript: Vitest fake timers and injected dependencies. See your language card in `docs/language-cards/`.
