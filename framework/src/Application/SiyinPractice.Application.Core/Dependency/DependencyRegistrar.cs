using SiyinPractice.Framework;
using SiyinPractice.Framework.Dependency;
using SiyinPractice.Interface.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SiyinPractice.Application.Core.Dependency
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            var appServices = App.FindClassesOfType<IAppService>();
            foreach (var appService in appServices)
            {
                services.AddScoped(appService.GetInterfaces().Last(), appService);
            }
        }
    }
}