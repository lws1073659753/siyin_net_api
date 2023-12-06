using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddContelWorks(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddContelWorksApp(builder.Configuration,
                                       builder.Environment.WebRootPath,
                                       builder.Environment.ContentRootPath);

            services.AddContelWorksControllers();

            services.AddCorsCors(builder.Configuration);

            services.AddHttpContextAccessor();

            services.AddCustomSwagger(builder.Configuration);

            services.AddAuthentication(builder.Configuration);

            services.AddSqlServer(builder.Configuration["ConnectionStrings:SqlServerConnection"]);

            services.AddAutoMapper();

            services.AddRegistrar();
        }

        private static void AddCorsCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray()
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}