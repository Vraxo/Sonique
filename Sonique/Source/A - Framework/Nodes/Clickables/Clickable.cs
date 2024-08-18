namespace Sonique;

public abstract class Clickable : Node2D
{
    public bool OnTopLeft = false;
    public bool OnTopRight = false;
    public int Layer = 0;

    protected ClickManager? clickManager;

    public override void Ready()
    {
        clickManager = GetNode<ClickManager>();

        if (clickManager == null)
        {
            Program.RootNode.AddChild(new ClickManager());
            clickManager = GetNode<ClickManager>();
        }

        clickManager.Add(this);
    }

    public override void Destroy()
    {
        clickManager.Remove(this);
    }

    public abstract bool IsMouseOver();
}