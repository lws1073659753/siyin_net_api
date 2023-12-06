using SiyinPractice.Domain.Core;
using SiyinPractice.Framework.Dependency;
using SiyinPractice.Framework.Uow;
using SiyinPractice.Infrastructure.EntityFramework.Repositories;
using SiyinPractice.Infrastructure.EntityFramework.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace SiyinPractice.Infrastructure.EntityFramework.Dependency
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContextService, ContextService>();
            // services.AddScoped<IAccountPasswordService, AccountPasswordService>();
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddScoped(typeof(IEfRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped(typeof(IBasicRepository<>), typeof(BasicRepository<>));
        }
    }
}