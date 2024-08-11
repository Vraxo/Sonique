using Raylib_cs;

namespace Sonique;

public partial class VerticalSlider : BaseSlider
{
    public VerticalSlider()
    {
        Size = new(10, 100);
        OriginPreset = OriginPreset.TopCenter;
    }

    public override void UpdatePercentageBasedOnMiddleButton(bool released = false)
    {
        float currentPosition = Grabber.GlobalPosition.Y;
        float minPos = GlobalPosition.Y - Origin.Y;
        float maxPos = minPos + Size.Y;

        float previousPercentage = Percentage;

        Percentage = (currentPosition - minPos) / (maxPos - minPos);
        Percentage = Math.Clamp(Percentage, 0, 1);

        if (Percentage != previousPercentage)
        {
            OnPercentageChanged();
        }
    }

    public override void MoveMiddleButton(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        float x = Grabber.GlobalPosition.X;
        float movementUnit = Size.Y / MathF.Abs(MaxExternalValue - 1);
        float y = Grabber.GlobalPosition.Y + direction * movementUnit;

        Grabber.GlobalPosition = new(x, y);
        UpdatePercentageBasedOnMiddleButton();
    }

    protected override void HandleClicks()
    {
        if (IsMouseOver())
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                float x = Grabber.GlobalPosition.X;
                float y = Raylib.GetMousePosition().Y;

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
            0,
            (int)Size.X,
            Color.Gray);
    }
}