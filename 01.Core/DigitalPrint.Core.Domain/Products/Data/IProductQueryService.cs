using System.Collections;
using DigitalPrint.Core.Domain.Products.DTOs;
using DigitalPrint.Core.Domain.Products.Queries;

namespace DigitalPrint.Core.Domain.Products.Data;

public interface IProductQueryService
{
    ProductDetail Query(GetActiveProduct query);
    IEnumerable<ProductSummary> Query(GetActiveProductList query);
    IEnumerable<ProductSummary> Query(GetProductForSpecificCreator query);
}