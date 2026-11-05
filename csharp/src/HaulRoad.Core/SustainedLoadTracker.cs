namespace HaulRoad.Core;

public sealed class SustainedLoadTracker
{
    private readonly TimeSpan _window;
    private readonly TimeProvider _clock;
    private LoadState? _candidate;
    private DateTimeOffset _candidateSince;

    public SustainedLoadTracker(TimeSpan window, TimeProvider clock)
    {
        _window = window;
        _clock = clock;
    }

    public LoadState Current { get; private set; } = LoadState.Empty;

    public void Observe(LoadState observed)
    {
        if (observed == Current)
        {
            _candidate = null;
            return;
        }

        if (_candidate != observed)
        {
            _candidate = observed;
            _candidateSince = _clock.GetUtcNow();
        }

        if (_clock.GetUtcNow() - _candidateSince >= _window)
        {
            Current = observed;
            _candidate = null;
        }
    }
}
