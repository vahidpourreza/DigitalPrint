using Framework.Domain.Data;
using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DigitalPrint.Infrastructures.Data.SqlServer;

public class ProductUnitOfWork : IUnitOfWork
{
    private readonly ProductDbContext _productDbContext;
    private readonly IEventSource _eventSource;

    public ProductUnitOfWork(ProductDbContext ProductDbContext , IEventSource eventSource)
    {
        _productDbContext = ProductDbContext;
        _eventSource = eventSource;
    }
    public int Commit()
    {
        var entityForSave = GetEntityForSave();
        int result = _productDbContext.SaveChanges();
        //SaveEvents(entityForSave);
        return result;
    }

    private void SaveEvents(List<EntityEntry> entityForSave)
    {
        foreach (var item in entityForSave)
        {
            var root = item.Entity as BaseAggregateRoot<Guid>;
            if (root != null)
            {
                var id = root.Id.ToString();
                var aggName = item.Entity.GetType().FullName;
                _eventSource.Save(aggName, id, root.GetEvents());
            }
        }
    }

    private List<EntityEntry> GetEntityForSave()
    {
        return _productDbContext.ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted)
            .ToList();
    }
}
