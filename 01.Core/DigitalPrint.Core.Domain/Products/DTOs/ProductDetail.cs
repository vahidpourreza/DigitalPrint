namespace DigitalPrint.Core.Domain.Products.DTOs;

public class ProductDetail
{
    public Guid ProductId { get; set; }
    public string Title { get; set; }
    public long Price { get; set; }
    public string Description { get; set; }
    public string SellersDisplayName { get; set; }
    public string[] PhotoUrls { get; set; }
}