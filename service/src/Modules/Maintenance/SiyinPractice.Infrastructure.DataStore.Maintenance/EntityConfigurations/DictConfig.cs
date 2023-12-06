using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.Maintenance.EntityConfigurations;

public class DictConfig : AbstractEntityTypeConfiguration<SysDict>
{
    public override void Configure(EntityTypeBuilder<SysDict> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Value).HasMaxLength(DictConsts.Value_MaxLength);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(DictConsts.Name_MaxLength);
        builder.Property(x => x.Pid).IsRequired();
    }
}