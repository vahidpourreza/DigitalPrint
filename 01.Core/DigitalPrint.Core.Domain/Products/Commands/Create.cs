namespace DigitalPrint.Core.Domain.Products.Commands;

public class Create
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public long Price { get; set; }
}