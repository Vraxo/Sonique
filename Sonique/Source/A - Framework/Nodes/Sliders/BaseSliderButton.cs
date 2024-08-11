using Raylib_cs;

namespace Sonique;

public abstract class BaseSliderButton : ClickableRectangle
{
    public Vector2 TextOrigin = Vector2.Zero;
    public string Text = "";
    public ButtonStyle Style = new();
    public bool Pressed = false;
    public Action<BaseSliderButton> OnUpdate = (button) => { };

    protected bool alreadyClicked = false;
    protected bool initialPositionSet = false;

    public BaseSliderButton()
    {
        Size = new(14, 14);
        InheritPosition = false;
    }

    public override void Start()
    {
        UpdatePosition(true);
        base.Start();
    }

    public override void Update()
    {
        UpdatePosition();
        CheckForClicks();
        Draw();
        OnUpdate(this);
        base.Update();
    }

    protected abstract void UpdatePosition(bool initial = false);

    private void CheckForClicks()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (!IsMouseOver())
            {
                alreadyClicked = true;
            }
        }

        if (IsMouseOver())
        {
            Style.Current = Style.Hover;

            if (Raylib.IsMouseButtonDown(MouseButton.Left) && !alreadyClicked && OnTopLeft)
            {
                OnTopLeft = false;
                Pressed = true;
                alreadyClicked = true;
            }

            if (Pressed)
            {
                Style.Current = Style.Pressed;
            }
        }
        else
        {
            Style.Current = Style.Default;
        }

        if (Pressed)
        {
            Style.Current = Style.Pressed;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            Pressed = false;
            Style.Current = Style.Default;
            alreadyClicked = false;
        }
    }

    private void Draw()
    {
        Rectangle rectangle = new()
        {
            Position = GlobalPosition - Origin,
            Size = Size
        };

        DrawInside(rectangle);
        DrawOutline(rectangle);
    }

    private void DrawInside(Rectangle rectangle)
    {
        Raylib.DrawRectangleRounded(
            rectangle,
            Style.Current.Roundness,
            (int)Size.Y,
            Style.Current.FillColor);
    }

    private void DrawOutline(Rectangle rectangle)
    {
        if (Style.Current.OutlineThickness > 0)
        {
            Raylib.DrawRectangleRoundedLines(
                rectangle,
                Style.Current.Roundness,
                (int)Size.Y,
                Style.Current.OutlineThickness,
                Style.Current.OutlineColor);
        }
    }
}