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
    public class AtchNoConfiguration : AbstractEntityTypeConfiguration<AtchNo>
    {
        public override void Configure(EntityTypeBuilder<AtchNo> builder)
        {
            base.Configure(builder);
            builder.ToTable("wm_atch");
            builder.Property(x => x.State).HasColumnName("State");
            builder.Property(x => x.AutomaticGeneration).HasColumnName("AutomaticGeneration");
            builder.Property(x => x.TotalInventory).HasColumnName("TotalInventory");
            builder.Property(x => x.ToBeCounted).HasColumnName("ToBeCounted");
            builder.Property(x => x.DeptName).HasColumnName("DeptName").HasMaxLength(64);
        }
    }
}
