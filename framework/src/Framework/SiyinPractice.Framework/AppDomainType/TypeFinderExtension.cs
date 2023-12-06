using SiyinPractice.Framework.AppDomainType;
using SiyinPractice.Framework.FileProvider;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TypeFinderExtension
    {
        public static ITypeFinder AddTypeFinder(this IServiceCollection services, ContelWorksFileProvider fileProvider)
        {
            var typeFinder = new WebAppTypeFinder(fileProvider);
            services.AddSingleton(typeof(ITypeFinder), typeFinder);
            return typeFinder;
        }

        public static ITypeFinder AddTypeFinder(this IServiceCollection services, string webRootPath, string contentRootPath)
        {
            return services.AddTypeFinder(new ContelWorksFileProvider(webRootPath, contentRootPath));
        }
    }
}