namespace SiyinPractice.Infrastructure.EntityFramework.EntityConfigurations
{
    [System.Obsolete]
    public partial interface IMappingConfiguration
    {
        /// <summary>
        /// Apply this mapping configuration
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the database context</param>
        void ApplyConfiguration(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder);
    }
}