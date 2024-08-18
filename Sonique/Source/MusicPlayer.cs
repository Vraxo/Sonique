using Raylib_cs;

namespace Sonique;

public class MusicPlayer : AudioPlayer
{
    private HorizontalSlider slider;

    public override void Start()
    {
        slider = Parent.GetChild<HorizontalSlider>("AudioSlider");
        slider.Released += OnSliderPercentageChanged;

        Parent.GetChild<HorizontalSlider>("VolumeSlider").Released += OnVolumeSliderPercentageChanged;

        base.Start();
    }

    private void OnVolumeSliderPercentageChanged(object? sender, float e)
    {
        Volume = e;
    }

    private void OnSliderPercentageChanged(object? sender, float e)
    {
        float timestamp = e * AudioLength;
        Play(timestamp);
    }

    public override void Update()
    {
        slider.MaxExternalValue = AudioLength;

        if (slider.Grabber != null)
        {
            if (!slider.Grabber.Pressed)
            {
                slider.ExternalValue = TimePlayed;

                // Calculate the percentage of the song played
                float percentage = TimePlayed / AudioLength;

                // Calculate the new X position of the middle button based on the slider's width
                float x = slider.Position.X + percentage * slider.Size.X;
                float y = slider.Grabber.GlobalPosition.Y;

                // Update the middle button's position
                slider.Grabber.GlobalPosition = new(x, y);
            }
        }

        base.Update();
    }
}