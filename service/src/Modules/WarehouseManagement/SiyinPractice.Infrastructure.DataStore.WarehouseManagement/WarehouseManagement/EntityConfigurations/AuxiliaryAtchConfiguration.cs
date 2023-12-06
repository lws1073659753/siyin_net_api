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
    public class AuxiliaryAtchConfiguration : AbstractEntityTypeConfiguration<AuxiliaryAtch>
    {
        public override void Configure(EntityTypeBuilder<AuxiliaryAtch> builder)
        {
            base.Configure(builder);
            builder.ToTable("wm_auxiliary_atch");
            builder.Property(x => x.State).HasColumnName("State").HasMaxLength(64);
            builder.Property(x => x.DeptName).HasColumnName("DeptName");
        }
    }


}
