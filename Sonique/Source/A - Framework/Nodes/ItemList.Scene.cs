namespace Sonique;

public partial class ItemList : ClickableRectangle
{
    public override void Build()
    {
        AddChild(new VerticalSlider
        {
            OnUpdate = (slider) =>
            {
                float x = Size.X - Origin.X - slider.Size.X - slider.Origin.X;
                float y = -Origin.Y + slider.Grabber.Size.Y * 2.5F; // *4

                slider.Position = new(x, y);

                //slider.Position.Y = slider.Grabber.Radius * 2.5F;

                slider.Size = new(slider.Size.X, Size.Y - slider.Grabber.Size.Y * 5); // * 8

                int numItemsBesidesThisPage = Items.Count - maxItemsShownAtOnce;

                slider.MaxExternalValue = numItemsBesidesThisPage > 0 ?
                                          numItemsBesidesThisPage :
                                          0;

                slider.ExternalValue = StartingIndex;
            }
        });
    }
}