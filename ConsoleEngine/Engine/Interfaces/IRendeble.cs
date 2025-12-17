namespace ConsoleEngine.Engine.Interfaces;

public interface IRenderable
{
    void SetPosition(int x, int y);
    void Render();
    int GetWidth();
    int GetHeight();
}