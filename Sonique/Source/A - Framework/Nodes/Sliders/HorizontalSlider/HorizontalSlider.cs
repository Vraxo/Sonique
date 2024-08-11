using Raylib_cs;

namespace Sonique;

public partial class HorizontalSlider : BaseSlider
{
    private bool wasPressed = false;

    public HorizontalSlider()
    {
        Size = new(100, 10);
        OriginPreset = OriginPreset.CenterLeft;
    }

    public override void UpdatePercentageBasedOnMiddleButton(bool released = false)
    {
        float currentPosition = Grabber.GlobalPosition.X;
        float minPos = GlobalPosition.X;
        float maxPos = minPos + Size.X;

        float previousValue = Percentage;

        Percentage = (currentPosition - minPos) / (maxPos - minPos);
        Percentage = Math.Clamp(Percentage, 0, 1);

        if (Percentage != previousValue)
        {
            OnPercentageChanged();
        }

        if (wasPressed && !Grabber.Pressed)
        {
            OnReleased();
        }

        wasPressed = Grabber.Pressed;
    }

    public override void MoveMiddleButton(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        float x = Grabber.GlobalPosition.X + direction * (Size.X / MaxExternalValue);
        float y = Grabber.GlobalPosition.Y;

        Grabber.GlobalPosition = new(x, y);
        UpdatePercentageBasedOnMiddleButton();
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
            }
        }
    }

    protected override void Draw()
    {
        Rectangle rectangle = new()
        {
            Position = GlobalPosition - Origin,
            Size = Size
        };

        Raylib.DrawRectangleRounded(
            rectangle,
            Style.Roundness,
            (int)Size.Y,
            Style.FillColor);
    }
}
