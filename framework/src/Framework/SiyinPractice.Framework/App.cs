using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SiyinPractice.Framework
{
    public class App
    {
        private App()
        {
        }

        public static IHttpContextAccessor HttpContextAccessor => GetService<IHttpContextAccessor>();

        public static IConfiguration Configuration { get; private set; }

        public static void AddConfiguation(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region type finder

        private static AppDomainType.ITypeFinder _typeFinder;

        public static void AddTypeFinder(AppDomainType.ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public static IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            ArgumentNullException.ThrowIfNull(_typeFinder);
            return _typeFinder.FindClassesOfType<T>(onlyConcreteClasses);
        }

        public static IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            ArgumentNullException.ThrowIfNull(_typeFinder);
            return _typeFinder.FindClassesOfType(assignTypeFrom, onlyConcreteClasses);
        }

        public static IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            ArgumentNullException.ThrowIfNull(_typeFinder);
            return _typeFinder.FindClassesOfType<T>(assemblies, onlyConcreteClasses);
        }

        public static IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            ArgumentNullException.ThrowIfNull(_typeFinder);
            return _typeFinder.FindClassesOfType(assignTypeFrom, assemblies, onlyConcreteClasses);
        }

        public static IList<Assembly> GetAssemblies()
        {
            ArgumentNullException.ThrowIfNull(_typeFinder);
            return _typeFinder.GetAssemblies();
        }

        #endregion type finder

        #region service provider

        private static IServiceProvider _serviceProvider { get; set; }

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="scope">Scope</param>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public static T GetService<T>(IServiceScope scope = null) where T : class
        {
            return (T)GetService(typeof(T), scope);
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <param name="scope">Scope</param>
        /// <returns>Resolved service</returns>
        public static object GetService(Type type, IServiceScope scope = null)
        {
            return GetServiceProvider(scope)?.GetService(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public static IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected static IServiceProvider GetServiceProvider(IServiceScope scope = null)
        {
            if (scope == null)
            {
                var accessor = _serviceProvider?.GetService<IHttpContextAccessor>();
                var context = accessor?.HttpContext;
                return context?.RequestServices ?? _serviceProvider;
            }
            return scope.ServiceProvider;
        }

        #endregion service provider

    }
}