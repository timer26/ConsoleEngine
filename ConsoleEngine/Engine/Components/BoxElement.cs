using ConsoleEngine.Engine.Interfaces;

namespace ConsoleEngine.Engine.Components;

public class BoxElement : IRenderable
{
    private int _x, _y;
    public int Width { get; set; }
    public int Height { get; set; }
    public string Title { get; set; }
    public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;

    public BoxElement(int width, int height, string title = "")
    {
        Width = width;
        Height = height;
        Title = title;
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Render()
    {
        Console.ForegroundColor = BorderColor;

        // Top border
        Console.SetCursorPosition(_x, _y);
        Console.Write("┌" + new string('─', Width - 2) + "┐");

        if (!string.IsNullOrEmpty(Title))
        {
            Console.SetCursorPosition(_x + 2, _y);
            Console.Write($" {Title} ");
        }

        // Sides
        for (int i = 1; i < Height - 1; i++)
        {
            Console.SetCursorPosition(_x, _y + i);
            Console.Write("│");
            Console.SetCursorPosition(_x + Width - 1, _y + i);
            Console.Write("│");
        }

        // Bottom border
        Console.SetCursorPosition(_x, _y + Height - 1);
        Console.Write("┌" + new string('─', Width - 2) + "┘");

        Console.ResetColor();
    }

    public int GetWidth() => Width;
    public int GetHeight() => Height;
}