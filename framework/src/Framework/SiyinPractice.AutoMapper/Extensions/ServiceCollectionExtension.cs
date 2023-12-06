using SiyinPractice.Framework;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Mapper;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configure =>
            {
                var configurations = App.FindClassesOfType<IObjectMapperConfigration>();
                var configurationInstances = configurations.Select(startup => (IObjectMapperConfigration)Activator.CreateInstance(startup));
                foreach (var instance in configurationInstances)
                {
                    var mappingData = instance.ObjectMapperCreaterBuilder();
                    foreach (var item in mappingData)
                    {
                        configure.CreateMap(item.SourceType, item.DestinationType);
                        if (item.TwoWay) configure.CreateMap(item.DestinationType, item.SourceType);
                    }
                }

                foreach (var assembly in App.GetAssemblies())
                {
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        var map = (ObjectMapAttribute)Attribute.GetCustomAttribute(type, typeof(ObjectMapAttribute));
                        if (map != null)
                        {
                            configure.CreateMap(type, map.TargetType);

                            if (map.TwoWay)
                            {
                                configure.CreateMap(map.TargetType, type);
                            }
                        }
                    }
                }
            });

            services.AddSingleton<IObjectMapper, AutoMapperObjectMapper>();
        }
    }
}