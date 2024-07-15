using DigitalPrint.Core.Domain.Products.Data;

namespace DigitalPrint.Infrastructures.Data.InMemory.Product;

public class InMemoryProdcutsRepository : IProductsRepository
{
    private readonly Dictionary<Guid, Core.Domain.Products.Entities.Product> db = new();
    public bool Exists(Guid id)
    {
        return db.ContainsKey(id);
    }

    public Core.Domain.Products.Entities.Product Load(Guid id)
    {
        return db[id];
    }

    public void Add(Core.Domain.Products.Entities.Product entity)
    {
        db[entity.Id] = entity;
    }
}