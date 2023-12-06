using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnmIntel.Infrastructure.DataStore.WarehouseManagement.WarehouseManagement.EntityConfigurations
{
    public class AuxiliaryInventoryHistoryConfiguration : AbstractEntityTypeConfiguration<AuxiliaryInventoryHistory>
    {
        public override void Configure(EntityTypeBuilder<AuxiliaryInventoryHistory> builder)
        {
            base.Configure(builder);
            builder.ToTable("wm_auxiliary_inventory_history");
            builder.Property(x => x.SysPn).HasColumnName("SysPn").HasMaxLength(64);
            builder.Property(x => x.PnQty).HasColumnName("PnQty");
            builder.Property(x => x.PnState).HasColumnName("PnState").HasMaxLength(64);
            builder.Property(x => x.SysBin).HasColumnName("SysBin").HasMaxLength(64);
            builder.Property(x => x.SysLocation).HasColumnName("SysLocation").HasMaxLength(64);
            builder.Property(x => x.CreateDept).HasColumnName("CreateDept").HasMaxLength(64);
        }
    }
}