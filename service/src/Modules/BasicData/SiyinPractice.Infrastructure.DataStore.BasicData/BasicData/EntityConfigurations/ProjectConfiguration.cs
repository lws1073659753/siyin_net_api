using SiyinPractice.Domain.BasicData;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiyinPractice.Infrastructure.DataStore.BasicData.BasicData.EntityConfigurations
{
    public class ProjectConfiguration : AbstractEntityTypeConfiguration<Project>
    {
       
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);
            builder.ToTable("base_project");
            builder.Property(x => x.BoxPrefix).HasColumnName("BoxPrefix");
            builder.Property(x => x.TAG_NOPrefix).HasColumnName("TAG_NOPrefix");
            builder.Property(x => x.SnReplace).HasColumnName("SnReplace");
            builder.Property(x => x.EndShield).HasColumnName("EndShield");
            builder.Property(x => x.IsPlantSn).HasColumnName("IsPlantSn");
            builder.Property(x => x.CreateDept).HasColumnName("CreateDept");
        }
    }
}
