using Raylib_cs;

namespace Sonique;

public partial class HorizontalSlider : BaseSlider
{
    public HorizontalSlider()
    {
        Size = new(100, 10);
        OriginPreset = OriginPreset.CenterLeft;
    }

    protected override void UpdatePercentage()
    {
        float currentPosition = Grabber.GlobalPosition.X;
        float minPos = GlobalPosition.X;
        float maxPos = minPos + Size.X;

        // Calculate and clamp the percentage
        Percentage = Math.Clamp((currentPosition - minPos) / (maxPos - minPos), 0, 1);
    }

    public override void MoveGrabber(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        float unit = Size.X / MaxExternalValue;
        float x = Grabber.GlobalPosition.X + direction * unit;
        float y = Grabber.GlobalPosition.Y;

        Grabber.GlobalPosition = new(x, y);

        UpdatePercentageBasedOnGrabber();
    }

    protected override void MoveGrabberTo(float percentage)
    {
        float x = GlobalPosition.X + percentage * Size.X;
        float y = GlobalPosition.Y;

        Grabber.GlobalPosition = new(x, y);
    }

    protected override void HandleClicks()
    {
        if (IsMouseOver())
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                float x = Raylib.GetMousePosition().X;
                float y = Grabber.GlobalPosition.Y;

                Grabber.GlobalPosition = new(x, y);
                Grabber.Pressed = true;
            }
        }
    }

    protected override void Draw()
    {
        Vector2 position = GlobalPosition - Origin;

        Rectangle emptyRectangle = new()
        {
            Position = position,
            Size = Size
        };

        DrawOutline(emptyRectangle, EmptyStyle.Current);

        Raylib.DrawRectangleRounded(
            emptyRectangle,
            EmptyStyle.Current.Roundness,
            (int)Size.Y,
            EmptyStyle.Current.FillColor);

        Rectangle filledRectangle = new()
        {
            Position = position,
            Size = new(Percentage * Size.X, Size.Y)
        };

        DrawOutline(filledRectangle, FilledStyle.Current);

        Raylib.DrawRectangleRounded(
            filledRectangle,
            FilledStyle.Current.Roundness,
            (int)Size.Y,
            FilledStyle.Current.FillColor);
    }

    private void DrawOutline(Rectangle rectangle, ButtonStateStyle style)
    {
        if (style.OutlineThickness <= 0)
        {
            return;
        }

        for (int i = 0; i <= style.OutlineThickness; i++)
        {
            Rectangle outlineRectangle = new()
            {
                Position = rectangle.Position - new(i, i),
                Size = new(rectangle.Size.X + i + 1, rectangle.Size.Y + i + 1)
            };

            Raylib.DrawRectangleRounded(
                outlineRectangle,
                style.Roundness,
                (int)rectangle.Size.Y,
                style.OutlineColor);
        }
    }
}