using ConsoleEngine.Engine.Components;
using ConsoleEngine.Engine.Core;

namespace ConsoleEngine.Engine.SceneManagment;

public abstract class Scene
{
    public Core.Engine? Engine { get; set; }
    protected Container RootContainer { get; set; }
    protected List<Ticker> Tickers { get; } = new();

    protected Scene()
    {
        RootContainer = new VContainer(0, 0, 120, 30);
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }

    public virtual void HandleInput(ConsoleKeyInfo key) { }

    public virtual void Update(double deltaTime)
    {
        foreach (var ticker in Tickers.ToList())
        {
            ticker.Update(deltaTime);
        }
    }

    public virtual void Render()
    {
        RootContainer.Render();
    }

    public void AddTicker(Ticker ticker)
    {
        Tickers.Add(ticker);
    }

    public void RemoveTicker(Ticker ticker)
    {
        Tickers.Remove(ticker);
    }
}