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
        float currentPosition = MiddleButton.GlobalPosition.X;
        float minPos = GlobalPosition.X;
        float maxPos = minPos + Size.X;

        float previousValue = Percentage;

        Percentage = (currentPosition - minPos) / (maxPos - minPos);
        Percentage = Math.Clamp(Percentage, 0, 1);

        if (Percentage != previousValue)
        {
            OnPercentageChanged();
        }

        if (wasPressed && !MiddleButton.Pressed)
        {
            OnReleased();
        }

        wasPressed = MiddleButton.Pressed;
    }

    public override void MoveMiddleButton(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        float x = MiddleButton.GlobalPosition.X + direction * (Size.X / MaxExternalValue);
        float y = MiddleButton.GlobalPosition.Y;

        MiddleButton.GlobalPosition = new(x, y);
        UpdatePercentageBasedOnMiddleButton();
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
