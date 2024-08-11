using Raylib_cs;

namespace Sonique;

class TexturedRectangle : Node2D
{
    public Texture2D Texture;
    public Action<TexturedRectangle> OnUpdate = (rectangle) => { };

    public override void Update()
    {
        Draw();
        OnUpdate(this);
        base.Update();
    }

    private void Draw()
    {
        Rectangle source = new(0, 0, Texture.Width, Texture.Height);
        Rectangle destination = new(GlobalPosition, Size);

        Raylib.DrawTexturePro(
            Texture,
            source,
            destination,
            Origin,
            0,
            Color.White);
    }
}