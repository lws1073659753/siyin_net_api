using System;

namespace SiyinPractice.Interface.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class OperateLogAttribute : Attribute
    {
        public string LogName { get; set; }
    }
}