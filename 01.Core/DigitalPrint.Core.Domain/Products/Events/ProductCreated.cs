using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.Products.Events;

public class ProductCreated : IEvent
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
}