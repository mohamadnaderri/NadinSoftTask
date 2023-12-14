using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoftTask.DomainModel.Product;

namespace NadinSoftTask.Infrastructure.Persistence.Mappings
{
    public class ProductMappings : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.ManufacturerEmail).HasMaxLength(50);
            builder.Property(p => p.ManufacturerPhoneNumber).HasMaxLength(11);

            builder.OwnsOne(q => q.Creator, c =>
            {
                c.Property(o => o.OperatorId).HasColumnName("OperatorId");
                c.Property(o => o.Name).HasColumnName("OperatorName");
            });

            builder.ToTable("Products");
        }
    }
}
