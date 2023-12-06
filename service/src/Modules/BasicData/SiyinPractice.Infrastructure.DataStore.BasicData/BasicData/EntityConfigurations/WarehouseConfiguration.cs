
using SiyinPractice.Domain.BasicData;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.BasicData.BasicData.EntityConfigurations
{
    public class WarehouseConfiguration : AbstractEntityTypeConfiguration<Warehouse>
    {
        public override void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            base.Configure(builder);
            builder.ToTable("base_warehouse");
            builder.Property(x => x.warehouseCode).HasColumnName("WarehouseCode").HasMaxLength(64);


           
        }
    }
}
