namespace HaulRoad.Core;

public sealed class SustainedLoadTracker
{
    private readonly TimeSpan _window;
    private LoadState? _candidate;
    private DateTime _candidateSince;

    public SustainedLoadTracker(TimeSpan window)
    {
        _window = window;
    }

    public LoadState Current { get; private set; } = LoadState.Empty;

    public void Observe(LoadState observed)
    {
        if (_candidate is null)
        {
            _candidate = observed;
            _candidateSince = DateTime.UtcNow;
        }

        if (DateTime.UtcNow - _candidateSince >= _window)
        {
            Current = observed;
        }
    }
}
