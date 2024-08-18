using Raylib_cs;
using System.Numerics;

namespace Sonique;

public partial class ItemList : ClickableRectangle
{
    public List<Node2D> Items = [];
    public Vector2 ItemSize = new(100, 20);
    public event EventHandler<int>? StartingIndexChanged;
    public VerticalSlider Slider;
    public Action<ItemList> OnUpdate = (list) => { };
    public int SliderButtonLayer = 0;
    public Action<ItemList> OnItemCountChanged = (list) => { };

    private int maxItemsShownAtOnce = 0;

    private int _startingIndex = 0;
    public int StartingIndex
    {
        get => _startingIndex;

        set
        {
            if (value < 0)
            {
                _startingIndex = 0;
            }
            else if (value > Items.Count - maxItemsShownAtOnce)
            {
                _startingIndex = Math.Max(0, Items.Count - maxItemsShownAtOnce);
            }
            else
            {
                _startingIndex = value;
            }
        }
    }

    public ItemList()
    {
        Size = new(250, 150);
    }

    public override void Ready()
    {
        Slider = GetChild<VerticalSlider>();
        Slider.PercentageChanged += OnSliderValueChanged;
        Slider.Layer = SliderButtonLayer;

        UpdateList(StartingIndex);

        base.Ready();
    }

    public override void Update()
    {
        HandleScrolling();
        OnUpdate(this);
        UpdateList(StartingIndex);
    }

    public void AddItem(Node2D item)
    {
        item.InheritsOrigin = true;
        Items.Add(item);
        AddChild(item);
        OnItemCountChanged(this);
    }

    public void RemoveItem(Node2D item)
    {
        Items.Remove(item);
        Children.Remove(item);
        item.Destroy();
        OnItemCountChanged(this);
    }

    public void RemoveItem(int index)
    {
        Node2D item = Items[index];
        RemoveItem(item);
    }

    private void OnSliderValueChanged(object? sender, float e)
    {
        //int newStartingIndex = GetStartingIndexBasedOnSliderValue(e);
        int newStartingIndex = (int)(sender as VerticalSlider).Value;
        StartingIndex = newStartingIndex;
    }

    private void MinimizeStartingIndex()
    {
        while (StartingIndex > Math.Max(0, Items.Count - maxItemsShownAtOnce))
        {
            StartingIndex--;
        }
    }

    private void UpdateMaxItemsShownAtOnce()
    {
        maxItemsShownAtOnce = (int)(Size.Y / ItemSize.Y);
    }

    private int GetStartingIndexBasedOnSliderValue(float sliderValue)
    {
        int numItemsBesidesThisPage = Items.Count - maxItemsShownAtOnce;
        return (int)Math.Floor(sliderValue * numItemsBesidesThisPage);
    }

    private void UpdateChildrenActivationAndPosition()
    {
        int j = 0;

        for (int i = 0; i < Items.Count; i++)
        {
            if (i >= StartingIndex && i < StartingIndex + maxItemsShownAtOnce)
            {
                Items[i].Activate();
                //Items[i].Position.Y = ItemSize.Y * j;
                Items[i].Position = new(Items[i].Position.X, ItemSize.Y * j);
                j++;
            }
            else
            {
                Items[i].Deactivate();
            }
        }
    }

    private void HandleScrolling()
    {
        bool isOnTop = Layer >= clickManager.MinLayer;

        if (!IsMouseOver() || !isOnTop)
        {
            return;
        }

        float mouseWheelMovement = Raylib.GetMouseWheelMove();

        if (mouseWheelMovement > 0)
        {
            UpdateList(StartingIndex - 1);
            Slider.MoveGrabber(-1);
        }
        else if (mouseWheelMovement < 0)
        {
            UpdateList(StartingIndex + 1);
            Slider.MoveGrabber(+1);
        }
    }

    private void UpdateList(int newStartingIndex)
    {
        MinimizeStartingIndex();
        UpdateMaxItemsShownAtOnce();

        StartingIndex = newStartingIndex;

        UpdateChildrenActivationAndPosition();

        StartingIndexChanged?.Invoke(this, StartingIndex);
    }
}