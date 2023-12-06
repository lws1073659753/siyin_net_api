using System;

namespace SiyinPractice.Framework.Security
{
    public class EncryptionService
    {
        public static string MD5(string sourceString, bool isLower = false)
        {
            if (string.IsNullOrEmpty(sourceString)) return String.Empty;
            return new HashGenerater().GetHashedString(HashType.MD5, sourceString, isLower);
        }
    }
}