using SiyinPractice.Domain.AccessControl;
using SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adnc.Usr.Repository.Entities.Config;

public class RoleMenuRelationConfig : AbstractEntityTypeConfiguration<SysRelation>
{
    public override void Configure(EntityTypeBuilder<SysRelation> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.RoleId).IsRequired();
        builder.Property(x => x.MenuId).IsRequired();
    }
}