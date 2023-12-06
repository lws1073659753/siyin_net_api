using Microsoft.Extensions.DependencyInjection;

namespace SiyinPractice.Framework.Dependency
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="typeFinder">Type finder</param>
        void Register(IServiceCollection services);
    }
}