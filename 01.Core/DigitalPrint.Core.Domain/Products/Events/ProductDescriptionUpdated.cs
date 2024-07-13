using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductDescriptionUpdated : IEvent
{
    public Guid Id { get; set; }
    public string Description { get; set; }

}