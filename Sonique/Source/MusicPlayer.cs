namespace Sonique;

public class MusicPlayer : AudioPlayer
{
    private HorizontalSlider slider;

    public override void Ready()
    {
        slider = Parent.GetChild<HorizontalSlider>("AudioSlider");
        slider.Released += OnSliderPercentageChanged;

        var volumeSlider = Parent.GetChild<HorizontalSlider>("VolumeSlider");
        volumeSlider.Released += OnVolumeSliderPercentageChanged;
        volumeSlider.MoveGrabberTo(1);

        base.Ready();
    }

    private void OnSliderPercentageChanged(object? sender, float e)
    {
        float timestamp = e * AudioLength;
        Play(timestamp);
    }

    private void OnVolumeSliderPercentageChanged(object? sender, float e)
    {
        Volume = e;
    }

    public override void Update()
    {
        var volumeSlider = Parent.GetChild<HorizontalSlider>("VolumeSlider");
        //volumeSlider.MoveGrabberTo(1);

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