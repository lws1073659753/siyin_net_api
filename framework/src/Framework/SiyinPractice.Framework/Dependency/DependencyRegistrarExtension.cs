using SiyinPractice.Framework;
using SiyinPractice.Framework.Dependency;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyRegistrarExtension
    {
        public static void AddRegistrar(this IServiceCollection services)
        {
            var startupConfigurations = App.FindClassesOfType<IDependencyRegistrar>();
            var startupinstances = startupConfigurations.Select(startup => (IDependencyRegistrar)Activator.CreateInstance(startup));
            foreach (var startupinstance in startupinstances)
                startupinstance.Register(services);
        }
    }
}