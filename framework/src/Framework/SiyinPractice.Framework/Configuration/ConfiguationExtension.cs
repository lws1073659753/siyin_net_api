using SiyinPractice.Framework.Configuration;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfiguationExtension
    {
        public static void AddConfiguation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            services.AddSingleton(appSettings);
        }
    }
}