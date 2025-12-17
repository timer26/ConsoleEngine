using ConsoleEngine.Engine.Components;
using ConsoleEngine.Engine.Core;
using ConsoleEngine.Engine.SceneManagment;

namespace ConsoleEngine;

// Program Entry Point
static class Program
{
    static void Main(string[] args)
    {
        var engine = new Engine.Core.Engine();
        engine.LoadScene(new MainMenuScene());
        engine.Run();

        // Restore cursor on exit
        Console.CursorVisible = true;
        Console.Clear();
    }
}

class MainMenuScene : Scene
{
    public override void OnEnter()
    {
        int termWidth = Engine?.Width ?? 80;
        int termHeight = Engine?.Height ?? 24;

        var vContainer = new VContainer(2, 2, termWidth - 4, termHeight - 4);
        RootContainer = vContainer;

        // Title
        var title = new TextElement("=== MAIN MENU ===")
        {
            Color = ConsoleColor.Cyan
        };
        vContainer.Add(title);

        // Menu box
        var menuBox = new BoxElement(60, 12, "Navigation")
        {
            BorderColor = ConsoleColor.Green
        };
        vContainer.Add(menuBox);

        // Menu items
        vContainer.Add(new TextElement(""));
        vContainer.Add(new TextElement("  [1] Counter Scene") { Color = ConsoleColor.Yellow });
        vContainer.Add(new TextElement("  [2] Grid Demo Scene") { Color = ConsoleColor.Yellow });
        vContainer.Add(new TextElement("  [3] Animation Scene") { Color = ConsoleColor.Yellow });
        vContainer.Add(new TextElement("  [4] Animation Demo Scene") { Color = ConsoleColor.Yellow });
        vContainer.Add(new TextElement(""));
        vContainer.Add(new TextElement("  [ESC] Exit") { Color = ConsoleColor.Red });
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                Engine?.LoadScene(new CounterScene());
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                Engine?.LoadScene(new GridDemoScene());
                break;
            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                Engine?.LoadScene(new AnimationScene());
                break;
            case ConsoleKey.Escape:
                Engine?.Stop();
                break;
        }
    }
}

// Scene 2: Counter Scene
class CounterScene : Scene
{
    private int _counter = 0;
    private TextElement? _counterText;
    private TextElement? _statusText;

    public override void OnEnter()
    {
        int termWidth = Engine?.Width ?? 80;
        int termHeight = Engine?.Height ?? 24;

        var vContainer = new VContainer(2, 2, termWidth - 4, termHeight - 4);
        RootContainer = vContainer;

        // Title
        vContainer.Add(new TextElement("=== COUNTER SCENE ===") { Color = ConsoleColor.Cyan });

        // Info box
        var box = new BoxElement(50, 8, "Counter Display")
        {
            BorderColor = ConsoleColor.Green
        };
        vContainer.Add(box);

        // Counter display
        vContainer.Add(new TextElement(""));
        _counterText = new TextElement($"  Count: {_counter}") { Color = ConsoleColor.White };
        vContainer.Add(_counterText);

        _statusText = new TextElement("  Auto-incrementing every second...") { Color = ConsoleColor.Gray };
        vContainer.Add(_statusText);

        // Controls
        vContainer.Add(new TextElement(""));
        var hContainer = new HContainer(2, 0, 100, 3);
        hContainer.Add(new TextElement("[SPACE] +10") { Color = ConsoleColor.Yellow });
        hContainer.Add(new TextElement("[R] Reset") { Color = ConsoleColor.Yellow });
        hContainer.Add(new TextElement("[0] Menu") { Color = ConsoleColor.Magenta });
        vContainer.Add(hContainer);

        // Add auto-increment ticker
        AddTicker(new Ticker(1.0, () =>
        {
            _counter++;
            if (_counterText != null)
                _counterText.Text = $"  Count: {_counter}";
        }));
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.Spacebar:
                _counter += 10;
                if (_counterText != null)
                    _counterText.Text = $"  Count: {_counter}";
                if (_statusText != null)
                    _statusText.Text = "  +10 boost applied!";
                break;
            case ConsoleKey.R:
                _counter = 0;
                if (_counterText != null)
                    _counterText.Text = $"  Count: {_counter}";
                if (_statusText != null)
                    _statusText.Text = "  Counter reset!";
                break;
            case ConsoleKey.D0:
            case ConsoleKey.NumPad0:
                Engine?.LoadScene(new MainMenuScene());
                break;
            case ConsoleKey.Escape:
                Engine?.Stop();
                break;
        }
    }
}

// Scene 3: Grid Demo Scene
class GridDemoScene : Scene
{
    public override void OnEnter()
    {
        int termWidth = Engine?.Width ?? 80;
        int termHeight = Engine?.Height ?? 24;

        var vContainer = new VContainer(2, 2, termWidth - 4, termHeight - 4);
        RootContainer = vContainer;

        // Title
        vContainer.Add(new TextElement("=== GRID DEMO SCENE ===") { Color = ConsoleColor.Cyan });

        // Grid container with colored boxes
        var gridContainer = new GridContainer(2, 0, 70, 15, 4) { SpacingX = 3, SpacingY = 2 };

        var colors = new[]
        {
            ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow,
            ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.DarkYellow
        };

        for (int i = 0; i < 8; i++)
        {
            var text = new TextElement($"[Box {i + 1}]") { Color = colors[i] };
            gridContainer.Add(text);
        }

        vContainer.Add(gridContainer);

        // Controls
        vContainer.Add(new TextElement(""));
        vContainer.Add(new TextElement("[0] Return to Menu") { Color = ConsoleColor.Magenta });
        vContainer.Add(new TextElement("[ESC] Exit") { Color = ConsoleColor.Red });
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.D0:
            case ConsoleKey.NumPad0:
                Engine?.LoadScene(new MainMenuScene());
                break;
            case ConsoleKey.Escape:
                Engine?.Stop();
                break;
        }
    }
}

// Scene 4: Animation Scene
class AnimationScene : Scene
{
    private TextElement? _animText;
    private int _frame = 0;
    private readonly string[] _frames = { "◐", "◓", "◑", "◒" };

    public override void OnEnter()
    {
        int termWidth = Engine?.Width ?? 80;
        int termHeight = Engine?.Height ?? 24;

        var vContainer = new VContainer(2, 2, termWidth - 4, termHeight - 4);
        RootContainer = vContainer;

        // Title
        vContainer.Add(new TextElement("=== ANIMATION SCENE ===") { Color = ConsoleColor.Cyan });

        // Animation box
        var box = new BoxElement(50, 8, "Spinning Animation")
        {
            BorderColor = ConsoleColor.Green
        };
        vContainer.Add(box);

        // Animation display
        vContainer.Add(new TextElement(""));
        _animText = new TextElement($"  {_frames[0]} Loading...") { Color = ConsoleColor.Yellow };
        vContainer.Add(_animText);

        // Add animation ticker (updates 4 times per second)
        AddTicker(new Ticker(0.25, () =>
        {
            _frame = (_frame + 1) % _frames.Length;
            if (_animText != null)
                _animText.Text = $"  {_frames[_frame]} Loading...";
        }));

        // Controls
        vContainer.Add(new TextElement(""));
        vContainer.Add(new TextElement("[0] Return to Menu") { Color = ConsoleColor.Magenta });
        vContainer.Add(new TextElement("[ESC] Exit") { Color = ConsoleColor.Red });
    }

    public override void HandleInput(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.D0:
            case ConsoleKey.NumPad0:
                Engine?.LoadScene(new MainMenuScene());
                break;
            case ConsoleKey.Escape:
                Engine?.Stop();
                break;
        }
    }
}