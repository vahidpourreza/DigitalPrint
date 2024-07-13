using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductPriceUpdated : IEvent
{
    public Guid Id { get; set; }
    public long Price { get; set; }

}