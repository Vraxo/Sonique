using Raylib_cs;

namespace Sonique;

public class FontLoader
{
    public Dictionary<string, Font> Fonts = [];

    private static FontLoader? instance;

    public static FontLoader Instance
    {
        get
        {
            instance ??= new();

            if (!File.Exists("Resources/RobotoMono.ttf"))
            {
                //byte[] defaultFont = Resources.RobotoMono;
                //File.WriteAllBytes("Resources/RobotoMono.ttf", defaultFont);
            }

            return instance;
        }
    }

    private FontLoader()
    {
        string name = "RobotoMono 32";
        Font font = Raylib.LoadFontEx("Resources/RobotoMono.ttf", 32, null, 0);
        Fonts.Add(name, font);

        Texture2D texture = Fonts[name].Texture;
        var filter = TextureFilter.Bilinear;
        Raylib.SetTextureFilter(texture, filter);
    }
}