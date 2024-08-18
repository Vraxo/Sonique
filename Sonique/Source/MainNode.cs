using Raylib_cs;

namespace Sonique;

public class MainNode : Node
{
    public string AudioPath;

    private MusicPlayer musicPlayer;

    public override void Build()
    {
        AddChild(new MusicPlayer
        {
            AutoPlay = false,
            Loop = true
        });

        AddChild(new HorizontalSlider()
        {
            Position = new(50, 0),
            HasButtons = false,
            OnUpdate = (slider) =>
            {
                float y = Raylib.GetScreenHeight() * 0.2f;
                slider.Position = new(slider.Position.X, y);

                float width = Raylib.GetScreenWidth() - 75;
                float height = slider.Size.Y;
                slider.Size = new(width, height);
            }
        }, "AudioSlider");

        AddChild(new HorizontalSlider()
        {
            Position = new(50, 0),
            HasButtons = false,
            OnUpdate = (slider) =>
            {
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
        }, "VolumeSlider");

        AddChild(new Button
        {
            Position = new(25, 20),
            Size = new(32, 32),
            Text = "||",
            OnUpdate = (button) =>
            {
                button.Position = new(button.Position.X, Raylib.GetScreenHeight() * 0.2f);
            }
        });
    }

    public override void Ready()
    {
        musicPlayer = GetChild<MusicPlayer>();

        musicPlayer.Audio = Raylib.LoadMusicStream(AudioPath);
        musicPlayer.Play();


        GetChild<Button>().LeftClicked += OnButtonLeftClicked;
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
}