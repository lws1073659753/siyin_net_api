using System;

namespace SiyinPractice.Shared.Core.Exceptions
{
    public class SiyinPracticeException : Exception
    {
        public string ResourceName { get; protected set; }

        public string[] Parameters { get; protected set; }

        public SiyinPracticeException()
        {
        }

        /// <summary>
        /// 创建一个ContelWorks异常
        /// </summary>
        /// <param name="realException">实际异常</param>
        public SiyinPracticeException(string resourceName, params string[] parameters)
        {
            ResourceName = resourceName;
            Parameters = parameters;
        }
    }
}