using SiyinPractice.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations
{
    [System.Obsolete]
    public abstract class BaseEntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            //builder.Property("Version").HasColumnName("INNERVERSION");
        }
    }
}