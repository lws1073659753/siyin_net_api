using SiyinPractice.Framework;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppExtension
    {
        public static void AddContelWorksApp(this IServiceCollection services,
                                                IConfiguration configuration,
                                                string webRootPath,
                                                string contentRootPath)
        {
            services.AddConfiguation(configuration);
            App.AddConfiguation(configuration);
            var typeFinder = services.AddTypeFinder(webRootPath, contentRootPath);
            App.AddTypeFinder(typeFinder);
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class AppExtension
    {
        public static void UseContelWorksApp(this IApplicationBuilder app)
        {
            App.SetServiceProvider(app.ApplicationServices);
        }
    }
}