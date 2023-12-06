using SiyinPractice.Shared.Core.Exceptions;
using System;

namespace SiyinPractice.Shared.Core.Utility
{
    public static class Validate
    {
        public static void Assert(bool isTrue, string message, params string[] args)
        {
            if (isTrue)
            {
                throw new Exception(string.Format(message, args));
            }
        }

        public static void Assert(bool isTrue, SiyinPracticeMessage SiyinPracticeMessage, params string[] args)
        {
            if (isTrue)
            {
                throw new SiyinPracticeException(SiyinPracticeMessage.ResourceName, args);
            }
        }
    }
}