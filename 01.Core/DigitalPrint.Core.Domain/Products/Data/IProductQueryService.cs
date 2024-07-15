using DigitalPrint.Core.Domain.Products.DTOs;
using DigitalPrint.Core.Domain.Products.Queries;

namespace DigitalPrint.Core.Domain.Products.Data;

public interface IProductQueryService
{
    ProductDetail Query(GetActiveProduct query);
    ProductSummary Query(GetActiveProductList query);
    ProductSummary Query(GetProductForSpecificCreator query);
}