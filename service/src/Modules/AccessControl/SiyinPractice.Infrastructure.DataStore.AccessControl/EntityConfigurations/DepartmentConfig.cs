using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.AccessControl.EntityConfigurations;

public class DetpConfig : AbstractEntityTypeConfiguration<SysDept>
{
    public override void Configure(EntityTypeBuilder<SysDept> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(DeptConsts.FullName_MaxLength);
        builder.Property(x => x.SimpleName).IsRequired().HasMaxLength(DeptConsts.SimpleName_MaxLength);
        builder.Property(x => x.Tips).HasMaxLength(DeptConsts.Tips_MaxLength);
        builder.Property(x => x.Pids).HasMaxLength(DeptConsts.Pids_MaxLength);
    }
}