using ConsoleEngine.Engine.Interfaces;

namespace ConsoleEngine.Engine.Components;

public abstract class Container : IRenderable
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<IRenderable> Children { get; } = new();

    protected Container(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public void Add(IRenderable child)
    {
        Children.Add(child);
    }

    public void Remove(IRenderable child)
    {
        Children.Remove(child);
    }

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public abstract void Render();
        
    public int GetWidth() => Width;
    public int GetHeight() => Height;
}