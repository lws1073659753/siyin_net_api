using SiyinPractice.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations
{
    [System.Obsolete]
    public abstract class AuditEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity> where TEntity : AuditEntity
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(64).IsRequired();
            builder.Property(x => x.Creator).HasColumnName("Creator").HasMaxLength(64);
            builder.Property(x => x.CreateTime).HasColumnName("CreateTime");
            builder.Property(x => x.Editor).HasColumnName("Editor").HasMaxLength(64);
            builder.Property(x => x.EditTime).HasColumnName("EditTime");
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}