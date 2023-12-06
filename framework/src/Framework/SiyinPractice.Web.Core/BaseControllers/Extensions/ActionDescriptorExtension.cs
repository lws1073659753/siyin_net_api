using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace SiyinPractice.Web.Core.Extensions
{
    public static class ActionDescriptorExtension
    {
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor ontrollerActionDescriptor) return ontrollerActionDescriptor;
            throw new Exception($"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
        }

        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }

        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
    }
}