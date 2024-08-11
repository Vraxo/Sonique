namespace Sonique;

public class Node2D : Node
{
    public Vector2 Position { get; set; }  = Vector2.Zero;
    public OriginPreset OriginPreset { get; set; } = OriginPreset.Center;
    public bool InheritPosition { get; set; } = true;
    public bool InheritsOrigin { get; set; } = false;
    public bool Visible { get; set; } = true;

    private Vector2 _size = Vector2.Zero;
    public Vector2 Size
    {
        get => _size;

        set
        {
            //UpdateOrigin();
            _size = value;
        }
    }

    private Vector2 _globalPosition = Vector2.Zero;
    public Vector2 GlobalPosition
    {
        get
        {
            if (Parent is Node2D parentNode)
            {
                if (InheritPosition)
                {
                    return parentNode.GlobalPosition + Position;
                }

                return _globalPosition;
            }
            else
            {
                return Position;
            }
        }

        set
        {
            _globalPosition = value;
        }
    }

    private Vector2 _origin = Vector2.Zero;
    public Vector2 Origin
    {
        get
        {
            if (InheritsOrigin)
            {
                if (Parent is Node2D parentNode)
                {
                    return parentNode.Origin;
                }
            }

            return _origin;
        }

        set
        {
            _origin = value;
        }
    }

    public override void Update()
    {
        UpdateOrigin();
    }

    private void UpdateOrigin()
    {
        Origin = OriginPreset switch
        {
            OriginPreset.Center => Size / 2,
            OriginPreset.CenterLeft => new(0, Size.Y / 2),
            OriginPreset.CenterRight => new(Size.X, Size.Y / 2),
            OriginPreset.TopLeft => new(0, 0),
            OriginPreset.TopRight => new(Size.X, 0),
            OriginPreset.TopCenter => new(Size.X / 2, 0),
            OriginPreset.BottomLeft => new(0, Size.Y),
            OriginPreset.BottomRight => Size,
            OriginPreset.BottomCenter => new(Size.X / 2, Size.Y),
            OriginPreset.None => Origin,
            _ => Origin,
        };
    }
}