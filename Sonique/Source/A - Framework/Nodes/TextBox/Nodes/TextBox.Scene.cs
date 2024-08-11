namespace Sonique;

public partial class TextBox : ClickableRectangle
{
    public override void Build()
    {
        AddChild(new TextBoxShape());

        AddChild(new TextBoxText());

        AddChild(new TextBoxCaret());
    }
}