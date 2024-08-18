using Raylib_cs;

namespace Sonique;

public class TextBoxShape : Node2D
{
    public TextBoxStyle Style;

    private TextBox parent;

    public override void Ready()
    {
        parent = GetParent<TextBox>();
    }

    public override void Update()
    {
        DrawShape();
        base.Update();
    }

    private void DrawShape()
    {
        Rectangle rectangle = new()
        {
            Position = parent.GlobalPosition - parent.Origin,
            Size = parent.Size
        };

        DrawRectangle(rectangle);
        DrawOutline(rectangle);
    }

    private void DrawRectangle(Rectangle rectangle)
    {
        Raylib.DrawRectangleRounded(
            rectangle,
            parent.Style.Current.Roundedness,
            (int)parent.Size.Y,
            parent.Style.Current.FillColor);
    }

    private void DrawOutline(Rectangle rectangle)
    {
        if (parent.Style.Current.OutlineThickness > 0)
        {
            Raylib.DrawRectangleRoundedLines(
                rectangle,
                parent.Style.Current.Roundedness,
                (int)Size.Y,
                parent.Style.Current.OutlineThickness,
                parent.Style.Current.OutlineColor);
        }
    }
}