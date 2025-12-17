namespace ConsoleEngine.Engine.Components;

public class VContainer : Container
{
    public int Spacing { get; set; } = 1;

    public VContainer(int x, int y, int width, int height) : base(x, y, width, height) { }

    public override void Render()
    {
        int currentY = Y;
        int totalHeight = 0;
            
        foreach (var child in Children)
        {
            // Position child relative to this container
            child.SetPosition(X, currentY);
            child.Render();
                
            int childHeight = child.GetHeight();
            currentY += childHeight + Spacing;
            totalHeight += childHeight + Spacing;
        }
            
        // Update container height based on content (optional)
        if (Children.Count > 0)
            Height = totalHeight - Spacing;
    }
}