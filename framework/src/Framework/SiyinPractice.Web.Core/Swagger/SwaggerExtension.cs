using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SiyinPractice.Web.Host", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入JWT令牌，例如：Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "eShopOnContainers - Ordering HTTP API",
            //        Version = "v1",
            //        Description = "The Ordering Service HTTP API"
            //    });
            //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Type = SecuritySchemeType.OAuth2,
            //        Flows = new OpenApiOAuthFlows()
            //        {
            //            Implicit = new OpenApiOAuthFlow()
            //            {
            //                AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
            //                TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
            //                Scopes = new Dictionary<string, string>()
            //            {
            //                { "orders", "Ordering API" }
            //            }
            //            }
            //        }
            //    });

            //    options.OperationFilter<AuthorizeCheckOperationFilter>();
            //});

            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerExtensions
    {
        public static void UseCustomrSwagger(this IApplicationBuilder app, Hosting.IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiyinPractice.Web.Host v1"));
            }
        }
    }
}