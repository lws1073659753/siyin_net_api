using SiyinPractice.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;

namespace SiyinPractice.Infrastructure.EntityFramework
{
    public class SiyinPracticeDbContext : DbContext
    {
        public SiyinPracticeDbContext(DbContextOptions<SiyinPracticeDbContext> options)
          : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.UseOracle(CmpEnvironment.Instance.DefaultConnection, b => b.UseOracleSQLCompatibility("11"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typeConfigurations = App.FindClassesOfType(typeof(IEntityTypeConfiguration<>));
            var assemblies = typeConfigurations.Select(x => x.Assembly).Distinct();
            foreach (var assembly in assemblies)
            {
                modelBuilder = modelBuilder.ApplyConfigurationsFromAssembly(assembly/*, x => !x.GenericTypeArguments.Any()*/);
            }

#if DEBUG
            var entities = modelBuilder.Model.GetEntityTypes().ToList();
            foreach (var item in entities)
            {
                //var tabtype = Type.GetType(item.ClrType.FullName);
                var tableType = item.ClrType.Assembly.GetType(item.ClrType.FullName);
                var properties = tableType.GetProperties();
                var descriptionAttrtable = tableType.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (descriptionAttrtable.Length > 0)
                {
                    modelBuilder.Entity(item.Name).HasComment(((DescriptionAttribute)descriptionAttrtable[0]).Description);
                }
                foreach (var property in properties)
                {
                    var descriptionAttr = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (descriptionAttr.Length > 0)
                    {
                        modelBuilder.Entity(item.Name).Property(property.Name).HasComment(((DescriptionAttribute)descriptionAttr[0]).Description);
                    }
                }
            }
#endif
        }
    }
}