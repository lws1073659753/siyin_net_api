using System;

namespace SiyinPractice.Framework.Security
{
    public class UserToken
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string RoeleIds { get; set; }
    }
}