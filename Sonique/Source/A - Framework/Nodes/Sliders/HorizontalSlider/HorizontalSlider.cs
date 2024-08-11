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

    public override void UpdatePercentageBasedOnGrabber(bool released = false)
    {
        float previousValue = Percentage;

        // Update the percentage based on the grabber's position
        UpdatePercentage();

        // If the percentage has changed, invoke the PercentageChanged event
        if (Percentage != previousValue)
        {
            OnPercentageChanged();
        }

        // Handle the Released event if the grabber was pressed and is now released
        if (wasPressed && !Grabber.Pressed)
        {
            OnReleased();
        }

        // Update the pressed state
        wasPressed = Grabber.Pressed;
    }

    protected override void UpdatePercentage()
    {
        float currentPosition = Grabber.GlobalPosition.X;
        float minPos = GlobalPosition.X;
        float maxPos = minPos + Size.X;

        // Calculate and clamp the percentage
        Percentage = Math.Clamp((currentPosition - minPos) / (maxPos - minPos), 0, 1);
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
        UpdatePercentageBasedOnGrabber();
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

        // Draw the empty part of the slider
        Raylib.DrawRectangleRounded(
            rectangle,
            EmptyStyle.Current.Roundness,
            (int)Size.Y,
            EmptyStyle.Current.FillColor);

        // Draw the filled part of the slider
        Rectangle filledRectangle = new Rectangle()
        {
            Position = rectangle.Position,
            Size = new Vector2(Percentage * rectangle.Size.X, rectangle.Size.Y)
        };

        Raylib.DrawRectangleRounded(
            filledRectangle,
            FilledStyle.Current.Roundness,
            (int)Size.Y,
            FilledStyle.Current.FillColor);

        // Draw the outline
        if (EmptyStyle.Current.OutlineThickness > 0)
        {
            Raylib.DrawRectangleRoundedLines(
                rectangle,
                EmptyStyle.Current.Roundness,
                (int)Size.Y,
                EmptyStyle.Current.OutlineThickness,
                EmptyStyle.Current.OutlineColor);
        }
    }
}