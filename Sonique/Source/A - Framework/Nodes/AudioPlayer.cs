using Raylib_cs;

namespace Sonique;

public class AudioPlayer : Node
{
    public Music Audio { get; set; }
    public bool AutoPlay { get; set; } = false;
    public bool Loop { get; set; } = false;
    public bool Playing => Raylib.IsMusicStreamPlaying(Audio);
    public float TimePlayed => Raylib.GetMusicTimePlayed(Audio);
    public float AudioLength => Raylib.GetMusicTimeLength(Audio);

    public override void Start()
    {
        if (AutoPlay)
        {
            Play();
        }
    }

    public override void Update()
    {
        Raylib.UpdateMusicStream(Audio);

        if (TimePlayed >= AudioLength)
        {
            if (Loop)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Play(float timestamp = 0)
    {
        Raylib.PlayMusicStream(Audio);
        Raylib.SeekMusicStream(Audio, timestamp);
    }

    public void Resume()
    {
        Raylib.ResumeMusicStream(Audio);
    }

    public void Pause()
    {
        Raylib.PauseMusicStream(Audio);
    }

    public void Stop()
    {
        Raylib.StopMusicStream(Audio);
    }
}