using Raylib_cs;

namespace Sonique;

public class Program(WindowData windowData, string rootNodePath)
{
    public Node RootNode;

    private readonly WindowData windowData = windowData;

    public void Run()
    {
        Initialize();
        RunLoop();
    }

    private void Initialize()
    {
        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }

        int width = (int)windowData.Resolution.X;
        int height = (int)windowData.Resolution.Y;

        SetWindowFlags();

        Raylib.InitWindow(width, height, windowData.Title);
        Raylib.SetWindowMinSize(width, height);
        Raylib.InitAudioDevice();

        Scene scene = new(rootNodePath);
        var mainNode = scene.Instantiate<MainNode>();
        RootNode = mainNode;
        RootNode.Program = this;

        RootNode.Start();
        //RootNode.Build();
    }

    private static void SetWindowFlags()
    {
        Raylib.SetConfigFlags(
            ConfigFlags.VSyncHint | 
            ConfigFlags.Msaa4xHint |
            ConfigFlags.HighDpiWindow |
            ConfigFlags.ResizableWindow |
            ConfigFlags.AlwaysRunWindow);
    }

    private void RunLoop()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
                Raylib.ClearBackground(windowData.ClearColor);
                RootNode.Process();
            Raylib.EndDrawing();

            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                Console.Clear();
                RootNode.PrintChildren();
            }
        }
    }
}