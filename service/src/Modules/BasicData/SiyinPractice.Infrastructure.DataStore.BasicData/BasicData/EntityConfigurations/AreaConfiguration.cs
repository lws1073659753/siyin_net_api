
using SiyinPractice.Domain.BasicData;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.ProjectManagement.BasicData.EntityConfigurations
{
    public class AreaConfiguration : AbstractEntityTypeConfiguration<Area>
    {
        public override void Configure(EntityTypeBuilder<Area> builder)
        {
            base.Configure(builder);
            builder.ToTable("base_area");
            builder.Property(x => x.areaCode).HasColumnName("AreaCode").HasMaxLength(64);


           
        }
    }
}
