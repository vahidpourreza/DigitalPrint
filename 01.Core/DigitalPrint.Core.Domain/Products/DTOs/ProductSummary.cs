namespace DigitalPrint.Core.Domain.Products.DTOs;

public class ProductSummary
{
    public Guid ProductId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string PhotoUrl { get; set; }
}