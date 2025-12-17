using System.Diagnostics;
using ConsoleEngine.Engine.SceneManagment;

namespace ConsoleEngine.Engine.Core;

public class Engine
{
    private Scene? _currentScene;
    private bool _running;
    private readonly Stopwatch _stopwatch = new();
    private double _deltaTime;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Engine(int width = 120, int height = 30)
    {
        Width = width;
        Height = height;
        Console.CursorVisible = false;

        // Only set window size on Windows
        if (OperatingSystem.IsWindows())
        {
            try
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(width, height);
            }
            catch
            {
                // Ignore if terminal doesn't support it
            }
        }

        // For Linux/Mac, use actual terminal size
        try
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }
        catch
        {
            // Fallback to requested size if we can't detect
            Width = width;
            Height = height;
        }
    }
    public void LoadScene(Scene scene)
    {
        _currentScene?.OnExit();
        _currentScene = scene;
        _currentScene.Engine = this;
        _currentScene.OnEnter();
    }

    public void Run()
    {
        _running = true;
        _stopwatch.Start();

        while (_running)
        {
            _deltaTime = _stopwatch.Elapsed.TotalSeconds;
            _stopwatch.Restart();

            // Handle input
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                _currentScene?.HandleInput(key);
            }

            // Update
            _currentScene?.Update(_deltaTime);

            // Render
            Console.Clear();
            _currentScene?.Render();

            Thread.Sleep(16); // ~60 FPS
        }
    }

    public void Stop() => _running = false;
}