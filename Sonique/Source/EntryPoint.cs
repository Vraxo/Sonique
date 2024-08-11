namespace Sonique;

public class EntryPoint
{
    [STAThread]
    public static void Main(string[] args)
    {
        WindowData windowData = new()
        {
            Title = "Sonique",
            Resolution = new(480, 180),
            ClearColor = new(57, 57, 57, 255)
        };

        MainNode rootNode = new()
        {
            AudioPath = args.Length > 0 ?
                        args[0] :
                        "Resources/Audio.mp3"
        };

        Program program = new(windowData, rootNode);
        program.Run();
    }
}