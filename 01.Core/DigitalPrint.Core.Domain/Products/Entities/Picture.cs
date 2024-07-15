using DigitalPrint.Core.Domain.Products.Events;
using DigitalPrint.Core.Domain.Products.ValueObjects;
using Framework.Domain.Entities;
using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Entities;

public class Picture : BaseEntity<Guid>
{
    #region Fields
    public PictureSize Size { get; private set; }
    public PictureUrl Location { get; private set; }
    public int Order { get; private set; }
    #endregion

    #region Constructors
    private Picture()
    {

    }
    public Picture(Action<IEvent> applier) : base(applier)
    {
    }
    #endregion

    #region Methods
    protected override void SetStateByEvent(IEvent @event)
    {
        switch (@event)
        {
            case PictureAddedToProduct e:
                Id = e.PictureId;
                Location = PictureUrl.FromString(e.Url);
                Size = new PictureSize(e.Height, e.Width);
                Order = e.Order;
                break;
            case ProductPictureResized e:
                Size = new PictureSize(e.Height, e.Width);
                break;
        }
    }
    public void Resize(PictureSize newSize)
    {
        SetStateByEvent(new ProductPictureResized
        {
            PictureId = Id,
            Height = newSize.Width,
            Width = newSize.Width
        });
    }
    #endregion
}