using DigitalPrint.Core.Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DigitalPrint.Core.Domain.Shared.ValueObjects;

namespace DigitalPrint.Infrastructures.Data.SqlServer.Product;

public class ProductConfig : IEntityTypeConfiguration<Core.Domain.Products.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Core.Domain.Products.Entities.Product> builder)
    {
        builder.Property(c => c.Price).HasConversion(c => c.Value.Value, d => ProductPrice.FromLong(d));
        builder.Property(c => c.CreatorId).HasConversion(c => c.Value.ToString(), d => UserId.FromString(d));
        builder.Property(c => c.Title).HasConversion(c => c.Value, d => ProductTitle.FromString(d));
        builder.Property(c => c.Description).HasConversion(c => c.Value, d => ProductDescription.FromString(d));
        builder.Property(c => c.Category).HasConversion(c => c.Value, d => ProductCategory.FromString(d));

    }
}