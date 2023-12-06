using ConnmIntel.Domain.Shared.Maintenance;
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
    public class BoxConfiguration : AbstractEntityTypeConfiguration<Box>
    {
        public override void Configure(EntityTypeBuilder<Box> builder)
        {
            base.Configure(builder);
            builder.ToTable("wm_box");
            builder.Property(x => x.State).HasColumnName("State");
            builder.Property(x => x.AutomaticGeneration).HasColumnName("AutomaticGeneration");
            builder.Property(x => x.Bin).HasColumnName("Bin");
            builder.Property(x => x.BoxCount).HasColumnName("BoxCount");
            builder.Property(x => x.Location).HasColumnName("Location");
            builder.Property(x => x.Pallet).HasColumnName("Pallet").HasMaxLength(64);
        }
    }
}
