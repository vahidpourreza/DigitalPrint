using DigitalPrint.Core.Domain.Products.Entities;
using DigitalPrint.Core.Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DigitalPrint.Infrastructures.Data.SqlServer.Product;

public class PictureConfig : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.Property(c => c.Location).HasConversion(c => c.Url, d => PictureUrl.FromString(d));
        builder.OwnsOne(c => c.Size);
    }
}