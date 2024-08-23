using Raylib_cs;

namespace Sonique;

public abstract class BaseGrabber : ClickableRectangle
{
    public Vector2 TextOrigin = Vector2.Zero;
    public string Text = "";
    public ButtonStyle Style = new();
    public bool Pressed = false;
    public Action<BaseGrabber> OnUpdate = (button) => { };

    protected bool alreadyClicked = false;
    protected bool initialPositionSet = false;

    public BaseGrabber()
    {
        Size = new(14, 14);
        InheritPosition = false;
    }

    public override void Ready()
    {
        UpdatePosition(true);
        base.Ready();
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
        if (!Visible)
        {
            return;
        }

        float x = (float)Math.Round(GlobalPosition.X);
        float y = (float)Math.Round(GlobalPosition.Y);

        Vector2 temporaryPosition = new(x, y);

        DrawOutline(temporaryPosition);
        DrawInside(temporaryPosition);
    }

    private void DrawInside(Vector2 position)
    {
        Rectangle rectangle = new()
        {
            Position = position - Origin,
            Size = Size
        };

        Raylib.DrawRectangleRounded(
            rectangle,
            Style.Current.Roundness,
            (int)Size.Y,
            Style.Current.FillColor);
    }

    private void DrawOutline(Vector2 position)
    {
        if (Style.Current.OutlineThickness < 0)
        {
            return;
        }

        for (int i = 0; i <= Style.Current.OutlineThickness; i++)
        {
            Rectangle rectangle = new()
            {
                Position = position - Origin - new Vector2(i, i),
                Size = new(Size.X + i + 1, Size.Y + i + 1)
            };

            Raylib.DrawRectangleRounded(
                rectangle,
                Style.Current.Roundness,
                (int)Size.Y,
                Style.Current.OutlineColor);
        }
    }
}