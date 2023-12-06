using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace SiyinPractice.Framework.Security
{
    public class UserTokenService
    {
        public static UserToken GetUserToken()
        {
            var token = new UserToken();
            var httpContextAccessor = App.GetService<IHttpContextAccessor>();
            token.UserId = GetUserId(httpContextAccessor);
            token.RoeleIds = GetRoeleIds(httpContextAccessor);
            token.UserName = httpContextAccessor.HttpContext.User.Identity.Name;
            if (httpContextAccessor != null && httpContextAccessor.HttpContext.Request.Headers.Keys.Contains("UserName"))
                token.UserName = httpContextAccessor.HttpContext.Request.Headers["UserName"].First();
            return token;
        }

        private static Guid GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            foreach (var claim in httpContextAccessor.HttpContext.User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    return Guid.Parse(claim.Value);
                }
            }

            return Guid.Empty;
        }

        public static string GetRoeleIds(IHttpContextAccessor httpContextAccessor)
        {
            foreach (var claim in httpContextAccessor.HttpContext.User.Claims)
            {
                if (claim.Type == "roleids" /*ClaimTypes.Role*/)
                {
                    return claim.Value;
                }
            }

            return string.Empty;
        }
    }
}