using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductCreated : IEvent
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }

}