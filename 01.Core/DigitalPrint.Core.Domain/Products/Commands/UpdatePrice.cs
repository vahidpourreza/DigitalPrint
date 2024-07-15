namespace DigitalPrint.Core.Domain.Products.Commands;

public class UpdatePrice
{
    public Guid Id { get; set; }
    public long Price { get; set; }
}