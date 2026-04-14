using Martyzz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Martyzz.Infrastructure.Data.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.Id).HasDefaultValueSql("NEWID()");
            builder.Property(P => P.Price).HasPrecision(18, 2);
            // Relationships
            builder
                .HasOne(P => P.ProductCategory)
                .WithMany(B => B.Products)
                .HasForeignKey(P => P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            builder
                .HasOne(P => P.Brand)
                .WithMany(B => B.Products)
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(P => P.CategoryId);
            builder.HasIndex(P => P.BrandId);
        }
    }
}
