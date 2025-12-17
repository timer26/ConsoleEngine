namespace ConsoleEngine.Engine.Components;

public class GridContainer : Container
{
    public int Columns { get; set; }
    public int SpacingX { get; set; } = 2;
    public int SpacingY { get; set; } = 1;

    public GridContainer(int x, int y, int width, int height, int columns) : base(x, y, width, height)
    {
        Columns = columns;
    }

    public override void Render()
    {
        if (Children.Count == 0) return;

        int cellWidth = (Width - (SpacingX * (Columns - 1))) / Columns;
        int currentX = X, currentY = Y;
        int col = 0;
        int rowHeight = 0;

        foreach (var child in Children)
        {
            // Position child relative to this container
            child.SetPosition(currentX, currentY);
            child.Render();

            rowHeight = Math.Max(rowHeight, child.GetHeight());
            col++;

            if (col >= Columns)
            {
                col = 0;
                currentX = X;
                currentY += rowHeight + SpacingY;
                rowHeight = 0;
            }
            else
            {
                currentX += cellWidth + SpacingX;
            }
        }
    }
}