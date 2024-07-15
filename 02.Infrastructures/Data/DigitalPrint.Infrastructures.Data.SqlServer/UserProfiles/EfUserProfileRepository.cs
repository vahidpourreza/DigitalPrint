using DigitalPrint.Core.Domain.UserProfiles.Data;
using DigitalPrint.Core.Domain.UserProfiles.Entities;

namespace DigitalPrint.Infrastructures.Data.SqlServer.UserProfiles;

public class EfUserProfileRepository : IUserProfileRepository, IDisposable
{
    private readonly ProductDbContext ProductDbContext;

    public EfUserProfileRepository(ProductDbContext ProductDbContext)
    {
        this.ProductDbContext = ProductDbContext;
    }
    public void Add(UserProfile entity)
    {
        ProductDbContext.UserProfiles.Add(entity);
    }

    public void Dispose()
    {
        ProductDbContext.Dispose();
    }

    public bool Exists(Guid id)
    {
        return ProductDbContext.UserProfiles.Any(c => c.Id == id);
    }

    public UserProfile Load(Guid id)
    {
        return ProductDbContext.UserProfiles.Find(id);
    }
}
