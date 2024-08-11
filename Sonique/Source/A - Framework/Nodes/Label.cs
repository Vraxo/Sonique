using Raylib_cs;

namespace Sonique;

public class Label : Node2D
{
    public string Text = "";
    public Color Color = Color.White;
    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.Fonts["RobotoMono 32"];
    public Action<Label> OnUpdate = (label) => { };

    public Label()
    {
        OriginPreset = OriginPreset.CenterLeft;
    }

    public override void Update()
    {
        UpdateSize();
        DrawText();
        OnUpdate(this);
        base.Update();
    }

    private void UpdateSize()
    {
        Size = Raylib.MeasureTextEx(Font, Text, FontSize, 1);
    }

    private void DrawText()
    {
        Raylib.DrawTextEx(
            Font, 
            Text, 
            GlobalPosition - Origin, 
            FontSize, 
            1, 
            Color);
    }
}