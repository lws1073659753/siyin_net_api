using SiyinPractice.Framework.Extensions;
using System.Reflection;

namespace SiyinPractice.Framework.Uow
{
    public static class TransactionExtension
    {
        public static TransactionAttribute GetTransactionAttribute(this MethodInfo methodInfo)
        {
            return methodInfo.GetAttribute<TransactionAttribute>();
        }
    }
}