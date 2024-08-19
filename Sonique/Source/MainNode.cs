using Raylib_cs;

namespace Sonique;

public class MainNode : Node
{
    public string AudioPath = "Resources/Audio.mp3";

    private MusicPlayer musicPlayer;

    public override void Build()
    {
        //AddChild(new MusicPlayer
        //{
        //    AutoPlay = false,
        //    Loop = true,
        //});

        //AddChild(new HorizontalSlider()
        //{
        //    Position = new(50, 0),
        //    HasButtons = false,
        //}, "AudioSlider");
        
        //AddChild(new HorizontalSlider()
        //{
        //    Position = new(50, 0),
        //    HasButtons = false,
        //    Percentage = 1,
        //}, "VolumeSlider");
        //
        //AddChild(new Button
        //{
        //    Position = new(25, 20),
        //    Size = new(32, 32),
        //    Text = "||",
        //});
    }

    public override void Ready()
    {
        musicPlayer = GetChild<MusicPlayer>();
        
        musicPlayer.Audio = Raylib.LoadMusicStream(AudioPath);
        musicPlayer.Play();
        musicPlayer.Pitch = 0.9f;
        
        GetChild<Button>().LeftClicked += OnButtonLeftClicked;
    }

    public override void Update()
    {
        UpdateAudioSlider();
        UpdateVolumeSlider();
        UpdateButton();
    }

    private void OnButtonLeftClicked(object? sender, EventArgs e)
    {
        if (musicPlayer.Playing)
        {
            musicPlayer.Pause();
        }
        else
        {
            musicPlayer.Resume();
        }

        var button = sender as Button;

        button.Text = button.Text == ">" ?
                      "||" :
                      ">";
    }

    private void UpdateAudioSlider()
    {
        var slider = GetChild<HorizontalSlider>("AudioSlider");

        float y = Raylib.GetScreenHeight() * 0.2f;
        slider.Position = new(slider.Position.X, y);

        float width = Raylib.GetScreenWidth() - 75;
        float height = slider.Size.Y;
        slider.Size = new(width, height);
    }

    private void UpdateVolumeSlider()
    {
        var slider = GetChild<HorizontalSlider>("VolumeSlider");

        float screenWidth = Raylib.GetScreenWidth();

        var audioSlider = GetChild<HorizontalSlider>("AudioSlider");

        float spaceBetweenAudioSliderAndBorder = screenWidth - audioSlider.Size.X - audioSlider.GlobalPosition.X;

        float x = screenWidth - slider.Size.X - spaceBetweenAudioSliderAndBorder;
        float y = Raylib.GetScreenHeight() * 0.5f;
        slider.Position = new(x, y);

        float width = screenWidth / 5;
        float height = slider.Size.Y;
        slider.Size = new(width, height);
    }

    private void UpdateButton()
    {
        var button = GetChild<Button>("Button");

        float x = button.Position.X;
        float y = Raylib.GetScreenHeight() * 0.2f;

        button.Position = new(x, y);
    }
}