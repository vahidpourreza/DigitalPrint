using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductSentForPublish : IEvent
{
    public Guid Id { get; set; }
}

