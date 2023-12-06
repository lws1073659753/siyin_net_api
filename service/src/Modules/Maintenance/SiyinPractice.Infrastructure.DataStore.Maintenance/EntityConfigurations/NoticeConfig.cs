using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SiyinPractice.Infrastructure.DataStore.Maintenance.EntityConfigurations;

public class NoticeConfig : AbstractEntityTypeConfiguration<SysNotice>
{
    public override void Configure(EntityTypeBuilder<SysNotice> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content).IsRequired().HasMaxLength(NoticeConsts.Content_MaxLength);
        builder.Property(x => x.Title).HasMaxLength(NoticeConsts.Title_MaxLength);
    }
}