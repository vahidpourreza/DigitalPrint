using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class PictureAddedToProduct : IEvent
{
    public Guid PictureId { get; set; }
    public string Url { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Order { get; set; }
}