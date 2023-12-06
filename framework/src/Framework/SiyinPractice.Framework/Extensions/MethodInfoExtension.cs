using System;
using System.Linq;
using System.Reflection;

namespace SiyinPractice.Framework.Extensions
{
    public static class MethodInfoExtension
    {
        public static T GetAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<T>().ToList();
            if (attrs.Any())
            {
                return attrs.FirstOrDefault();
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<T>().ToList();
            if (attrs.Any())
            {
                return attrs.FirstOrDefault();
            }

            return null;
        }
    }
}