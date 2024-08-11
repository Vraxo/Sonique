namespace Sonique;

public abstract class BaseSlider : Node2D
{
    public float Percentage = 0;
    public float MaxExternalValue = 0;
    public float Value 
    {
        get
        {
            return MathF.Ceiling(Percentage * MaxExternalValue);
        } 
    }
    public bool HasButtons = true;
    public int Layer = 0;
    public SliderStyle Style = new();
    public BaseSliderButton MiddleButton;
    public Action<BaseSlider> OnUpdate = (slider) => { };
    public event EventHandler<float>? PercentageChanged;
    public event EventHandler<float>? Released;

    private float _externalValue = 0;
    public float ExternalValue
    {
        get => _externalValue;

        set
        {
            if (_externalValue != value)
            {
                _externalValue = value;

                //float minPos = GlobalPosition.Y - Origin.Y;
                //float maxPos = minPos + Size.Y;

                //float x = MiddleButton.GlobalPosition.X;
                //float y = ExternalValue / MaxExternalValue * maxPos;

                //MiddleButton.GlobalPosition = new(x, y);
            }
        }
    }

    public override void Start()
    {
        MiddleButton = GetChild<BaseSliderButton>("MiddleButton");

        var decrementButton = GetChild<Button>("DecrementButton");
        var incrementButton = GetChild<Button>("IncrementButton");

        decrementButton.LeftClicked += OnDecrementButtonLeftClicked;
        incrementButton.LeftClicked += OnIncrementButtonLeftClicked;

        MiddleButton.Layer = Layer;
        decrementButton.Layer = Layer;
        incrementButton.Layer = Layer;

        if (!HasButtons)
        {
            decrementButton.Deactivate();
            incrementButton.Deactivate();
        }
    }

    public override void Update()
    {
        Draw();
        OnUpdate(this);
        base.Update();
    }

    public abstract void UpdatePercentageBasedOnMiddleButton(bool released = false);

    private void OnDecrementButtonLeftClicked(object? sender, EventArgs e)
    {
        MoveMiddleButton(-1);
    }

    private void OnIncrementButtonLeftClicked(object? sender, EventArgs e)
    {
        MoveMiddleButton(1);
    }

    public abstract void MoveMiddleButton(int direction);

    protected abstract void Draw();

    protected void OnPercentageChanged()
    {
        PercentageChanged?.Invoke(this, Percentage);
    }

    protected void OnReleased()
    {
        Released?.Invoke(this, Percentage);
    }
}