using System;

namespace SiyinPractice.Framework.Uow
{
    public class TransactionAttribute : Attribute
    {
        public bool ReadOnly { get; set; }
    }
}