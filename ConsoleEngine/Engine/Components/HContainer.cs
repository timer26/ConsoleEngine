namespace ConsoleEngine.Engine.Components;

public class HContainer : Container
{
    public int Spacing { get; set; } = 2;

    public HContainer(int x, int y, int width, int height) : base(x, y, width, height) { }

    public override void Render()
    {
        int currentX = X;
        int maxHeight = 0;
            
        foreach (var child in Children)
        {
            // Position child relative to this container
            child.SetPosition(currentX, Y);
            child.Render();
                
            int childWidth = child.GetWidth();
            currentX += childWidth + Spacing;
            maxHeight = Math.Max(maxHeight, child.GetHeight());
        }
            
        // Update container dimensions based on content (optional)
        if (Children.Count > 0)
        {
            Width = currentX - X - Spacing;
            Height = maxHeight;
        }
    }
}