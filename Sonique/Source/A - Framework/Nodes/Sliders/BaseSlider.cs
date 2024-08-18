namespace Sonique;

public abstract class BaseSlider : ClickableRectangle
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
    public ButtonStyle FilledStyle = new();
    public ButtonStyle EmptyStyle = new();
    public BaseSliderButton Grabber;
    public Action<BaseSlider> OnUpdate = (slider) => { };
    public event EventHandler<float>? PercentageChanged;
    public event EventHandler<float>? Released;

    protected bool wasPressed = false;

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

                //float x = Grabber.GlobalPosition.X;
                //float y = ExternalValue / MaxExternalValue * maxPos;

                //Grabber.GlobalPosition = new(x, y);
            }
        }
    }

    public BaseSlider()
    {
        EmptyStyle.FillColor = new(101, 101, 101, 255);
        FilledStyle.FillColor = new(71, 114, 179, 255);
    }

    public override void Start()
    {
        Grabber = GetChild<BaseSliderButton>("MiddleButton");
        Grabber.Layer = Layer;

        var decrementButton = GetChild<Button>("DecrementButton");
        var incrementButton = GetChild<Button>("IncrementButton");

        if (!HasButtons)
        {
            decrementButton.Deactivate();
            incrementButton.Deactivate();
        }
        else
        {
            decrementButton.LeftClicked += OnDecrementButtonLeftClicked;
            incrementButton.LeftClicked += OnIncrementButtonLeftClicked;

            decrementButton.Layer = Layer;
            incrementButton.Layer = Layer;
        }

    }

    public override void Update()
    {
        UpdatePercentage();
        HandleClicks();
        Draw();
        OnUpdate(this);
        base.Update();
    }

    public void UpdatePercentageBasedOnGrabber()
    {
        float previousValue = Percentage;

        // Update the percentage based on the grabber's position
        UpdatePercentage();

        // If the percentage has changed, invoke the PercentageChanged event
        if (Percentage != previousValue)
        {
            OnPercentageChanged();
        }

        // Handle the Released event if the grabber was pressed and is now released
        if (wasPressed && !Grabber.Pressed)
        {
            OnReleased();
        }

        // Update the pressed state
        wasPressed = Grabber.Pressed;
    }

    public abstract void MoveMiddleButton(int direction);

    private void OnDecrementButtonLeftClicked(object? sender, EventArgs e)
    {
        MoveMiddleButton(-1);
    }

    private void OnIncrementButtonLeftClicked(object? sender, EventArgs e)
    {
        MoveMiddleButton(1);
    }

    protected abstract void UpdatePercentage();

    protected abstract void HandleClicks();

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