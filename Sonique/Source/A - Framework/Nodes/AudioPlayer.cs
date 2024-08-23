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

    public float Volume
    {
        set
        {
            Raylib.SetMusicVolume(Audio, value);
        }
    }

    public float Pitch
    {
        set
        {
            Raylib.SetMusicPitch(Audio, value);
        }
    }

    public float Pan
    {
        set
        {
            Raylib.SetMusicPan(Audio, value);
        }
    }

    public override void Ready()
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

    public void Play(float timestamp = 0.1f)
    {
        timestamp = Math.Clamp(timestamp, 0.1f, AudioLength);

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

    public void Seek(float timestamp)
    {
        timestamp = Math.Clamp(timestamp, 0.1f, AudioLength);

        Raylib.SeekMusicStream(Audio, timestamp);
    }
}