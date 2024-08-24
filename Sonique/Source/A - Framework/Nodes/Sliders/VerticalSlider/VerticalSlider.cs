using Raylib_cs;

namespace Sonique;

public partial class VerticalSlider : BaseSlider
{
    public VerticalSlider()
    {
        Size = new(10, 100);
        OriginPreset = OriginPreset.TopCenter;
    }

    protected override void UpdatePercentage()
    {
        float currentPosition = Grabber.GlobalPosition.Y;
        float minPos = GlobalPosition.Y - Origin.Y;
        float maxPos = minPos + Size.Y;

        // Calculate and clamp the percentage
        Percentage = Math.Clamp((currentPosition - minPos) / (maxPos - minPos), 0, 1);
    }

    public override void MoveGrabber(int direction)
    {
        if (MaxExternalValue == 0)
        {
            return;
        }

        Console.WriteLine(MaxExternalValue);

        float x = Grabber.GlobalPosition.X;
        float movementUnit = Size.Y / MathF.Abs(MaxExternalValue);
        float y = Grabber.GlobalPosition.Y + direction * movementUnit;

        Grabber.GlobalPosition = new(x, y);
        UpdatePercentageBasedOnGrabber();
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
                Grabber.Pressed = true;
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
        Rectangle filledRectangle = new()
        {
            Position = new(rectangle.Position.X, rectangle.Position.Y + (1 - Percentage) * rectangle.Size.Y),
            Size = new(rectangle.Size.X, Percentage * rectangle.Size.Y)
        };

        Raylib.DrawRectangleRounded(
            filledRectangle,
            FilledStyle.Current.Roundness,
            (int)Size.X,
            FilledStyle.Current.FillColor);

        // Draw the outline
        if (EmptyStyle.Current.OutlineThickness > 0)
        {
            Raylib.DrawRectangleRoundedLines(
                rectangle,
                EmptyStyle.Current.Roundness,
                (int)Size.X,
                EmptyStyle.Current.OutlineThickness,
                EmptyStyle.Current.OutlineColor);
        }
    }

    protected override void MoveGrabberTo(float percentage)
    {

    }
}