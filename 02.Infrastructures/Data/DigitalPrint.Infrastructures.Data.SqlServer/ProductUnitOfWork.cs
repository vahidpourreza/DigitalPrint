using Framework.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DigitalPrint.Infrastructures.Data.SqlServer;

public class ProductUnitOfWork : IUnitOfWork
{
    private readonly ProductDbContext ProductDbContext;

    public ProductUnitOfWork(ProductDbContext ProductDbContext)
    {
        this.ProductDbContext = ProductDbContext;
    }
    public int Commit()
    {
       // var entityForSave = GetEntityForSave();
        int result = ProductDbContext.SaveChanges();
      //  SaveEvents(entityForSave);
        return result; 
    }

    //private void SaveEvents(List<EntityEntry> entityForSave)
    //{
    //    foreach (var item in entityForSave)
    //    {
    //        var root = item.Entity as BaseAggregateRoot<Guid>;
    //        if (root != null)
    //        {
    //            var id = root.Id.ToString();
    //            var aggName = item.Entity.GetType().FullName;
    //            eventSource.Save(aggName, id, root.GetEvents());
    //        }
    //    }
    //}

    //private List<EntityEntry> GetEntityForSave()
    //{
    //    return ProductDbContext.ChangeTracker
    //        .Entries()
    //        .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted)
    //        .ToList();
    //}
}
