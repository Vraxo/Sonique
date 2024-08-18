using Raylib_cs;

namespace Sonique;

public class TextBoxText : Node2D
{
    public TextBoxStyle Style;

    private TextBox parent;

    public override void Ready()
    {
        parent = GetParent<TextBox>();
    }

    public override void Update()
    {
        Draw();
    }

    private void Draw()
    {
        Raylib.DrawTextEx(
            parent.Style.Current.Font,
            parent.Text,
            GetPosition(),
            parent.Style.Current.FontSize,
            parent.Style.Current.Spacing,
            parent.Style.Current.TextColor);
    }

    private Vector2 GetPosition()
    {
        Vector2 position = new(GetX(), GetY());
        return position;
    }

    private int GetX()
    {
        int x = (int)(GlobalPosition.X - parent.Origin.X + parent.Style.Current.Padding);
        return x;
    }

    private int GetY()
    {
        int halfFontHeight = GetHalfFontHeight();
        int y = (int)(GlobalPosition.Y + (parent.Size.Y / 2) - halfFontHeight - parent.Origin.Y);
        return y;
    }

    private int GetHalfFontHeight()
    {
        Font font = parent.Style.Current.Font;
        string text = parent.Text;
        uint fontSize = parent.Style.Current.FontSize;

        int halfFontHeight = (int)(Raylib.MeasureTextEx(font, text, fontSize, 1).Y / 2);
        return halfFontHeight;
    }
}