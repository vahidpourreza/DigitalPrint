using DigitalPrint.Core.Domain.Products.Data;

namespace DigitalPrint.Infrastructures.Data.SqlServer.Product;

public class EfProductsRepository : IProductsRepository, IDisposable
{
    private readonly ProductDbContext ProductDbContext;

    public EfProductsRepository(ProductDbContext ProductDbContext)
    {
        this.ProductDbContext = ProductDbContext;
    }

    public void Dispose()
    {
        ProductDbContext.Dispose();
    }

    public bool Exists(Guid id)
    {
        return ProductDbContext.Products.Any(c => c.Id == id);
    }

    public Core.Domain.Products.Entities.Product Load(Guid id)
    {
        return ProductDbContext.Products.Find(id);
    }

    public void Add(Core.Domain.Products.Entities.Product entity)
    {
        ProductDbContext.Products.Add(entity);

    }
}