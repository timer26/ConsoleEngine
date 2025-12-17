
namespace ConsoleEngine.Engine.Core;

public class Ticker
{
    private double _elapsed;
    private readonly double _interval;
    private readonly Action _callback;
    private readonly bool _repeat;

    public bool IsActive { get; private set; } = true;

    public Ticker(double interval, Action callback, bool repeat = true)
    {
        _interval = interval;
        _callback = callback;
        _repeat = repeat;
    }

    public void Update(double deltaTime)
    {
        if (!IsActive) return;

        _elapsed += deltaTime;
        if (_elapsed >= _interval)
        {
            _callback?.Invoke();
            _elapsed = 0;

            if (!_repeat)
                IsActive = false;
        }
    }

    public void Stop() => IsActive = false;
    public void Reset() => _elapsed = 0;
}