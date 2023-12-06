using SiyinPractice.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, string connectiongString)
        {
            services.AddDbContext<DbContext, SiyinPracticeDbContext>(options =>
                                             options
                                             .UseSqlServer(connectiongString, p => p.MigrationsAssembly(typeof(ServiceCollectionExtension).Assembly.GetName().Name))
                                             //.UseLazyLoadingProxies()
                                             .EnableSensitiveDataLogging()
                                            );

            return services;
        }

        public static IServiceCollection AddSqlServer(this IServiceCollection services, string connectiongString, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<DbContext, SiyinPracticeDbContext>(options =>
                                             options
                                             .UseSqlServer(connectiongString, p => p.MigrationsAssembly(typeof(ServiceCollectionExtension).Assembly.GetName().Name))
                                             .UseLazyLoadingProxies()
                                             .EnableSensitiveDataLogging()
                                             .UseLoggerFactory(loggerFactory)
                                            );

            return services;
        }
    }
}