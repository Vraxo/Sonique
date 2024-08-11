using Raylib_cs;

namespace Sonique;

public class MusicPlayer : AudioPlayer
{
    private HorizontalSlider slider;

    public override void Start()
    {
        slider = Parent.GetChild<HorizontalSlider>();
        slider.Released += OnSliderPercentageChanged;
        slider.MaxExternalValue = Raylib.GetMusicTimeLength(Audio);

        base.Start();
    }

    private void OnSliderPercentageChanged(object? sender, float e)
    {
        Play(e * AudioLength);
        Console.WriteLine(e);
    }

    public override void Update()
    {
        if (slider.MiddleButton != null)
        {
            if (!slider.MiddleButton.Pressed)
            {
                slider.ExternalValue = TimePlayed;

                // Calculate the percentage of the song played
                float percentage = TimePlayed / AudioLength;

                // Calculate the new X position of the middle button based on the slider's width
                float x = slider.Position.X + percentage * slider.Size.X;
                float y = slider.MiddleButton.GlobalPosition.Y;

                // Update the middle button's position
                slider.MiddleButton.GlobalPosition = new(x, y);
            }
        }

        base.Update();
    }
}