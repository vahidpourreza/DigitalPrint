namespace DigitalPrint.Core.Domain.Products.Queries;

public class GetProductForSpecificCreator
{
    public Guid CreatorUserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}