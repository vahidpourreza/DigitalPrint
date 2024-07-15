using DigitalPrint.Core.Domain.Products.Entities;

namespace DigitalPrint.Core.Domain.Products.Data;

public interface IProductsRepository
{
    bool Exists(Guid id);
    Product Load(Guid id);
    void Add(Product entity);
}
