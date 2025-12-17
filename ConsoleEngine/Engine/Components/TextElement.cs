using ConsoleEngine.Engine.Interfaces;

namespace ConsoleEngine.Engine.Components;

public class TextElement : IRenderable
{
    private int _x, _y;
    public string Text { get; set; }
    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public TextElement(string text)
    {
        Text = text;
    }

    public void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Render()
    {
        if (_y >= 0 && _y < Console.WindowHeight && _x >= 0 && _x < Console.WindowWidth)
        {
            Console.SetCursorPosition(_x, _y);
            Console.ForegroundColor = Color;
            Console.Write(Text);
            Console.ResetColor();
        }
    }
        
    public int GetWidth() => Text.Length;
    public int GetHeight() => 1;
}