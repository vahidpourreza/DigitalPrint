using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using DigitalPrint.Core.Domain.UserProfiles.Entities;

namespace DigitalPrint.Infrastructures.Data.SqlServer;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    }

    public DbSet<Core.Domain.Products.Entities.Product> Products { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

}