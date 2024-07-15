using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductPictureResized : IEvent
{
    public Guid PictureId { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

}