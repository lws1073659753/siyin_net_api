using ConnmIntel.Domain.WarehouseManagement;
using ConnmIntel.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnmIntel.Infrastructure.DataStore.WarehouseManagement.WarehouseManagement.EntityConfigurations
{
    public class InventoryConfiguration : AbstractEntityTypeConfiguration<Inventory>
    {
        public override void Configure(EntityTypeBuilder<Inventory> builder)
        {
            base.Configure(builder);
            builder.ToTable("wm_inventory");
            builder.Property(x => x.PiNum).HasColumnName("PiNum");
            builder.Property(x => x.PiDpt).HasColumnName("PiDpt");
            builder.Property(x => x.SysOrgSn).HasColumnName("SysOrgSn");
            builder.Property(x => x.SysOrgPn).HasColumnName("SysOrgPn");
            builder.Property(x => x.SysSn).HasColumnName("SysSn");
            builder.Property(x => x.SysPn).HasColumnName("SysPn");
            builder.Property(x => x.ScanSn).HasColumnName("ScanSn");
            builder.Property(x => x.ScanPn).HasColumnName("ScanPn");
            builder.Property(x => x.PiProject).HasColumnName("PiProject");
            builder.Property(x => x.SysBin).HasColumnName("SysBin");
            builder.Property(x => x.SysLocation).HasColumnName("SysLocation");
            builder.Property(x => x.Source).HasColumnName("Source");
            builder.Property(x => x.AccountBook).HasColumnName("AccountBook");
            builder.Property(x => x.FilingNo).HasColumnName("FilingNo");
            builder.Property(x => x.CreateDept).HasColumnName("CreateDept");
            builder.Property(x => x.BoxName).HasColumnName("BoxName");
            builder.Property(x => x.ScanPallet).HasColumnName("ScanPallet");
            builder.Property(x => x.AutomaticTag).HasColumnName("AutomaticTag");
            builder.Property(x => x.ProjectName).HasColumnName("ProjectName").HasMaxLength(255);
        }
    }
}
