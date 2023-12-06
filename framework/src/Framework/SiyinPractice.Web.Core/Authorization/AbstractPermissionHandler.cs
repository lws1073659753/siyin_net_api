using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace SiyinPractice.Web.Core.Authorization;

public abstract class AbstractPermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.Identity.IsAuthenticated && context.Resource is HttpContext httpContext)
        {
            var authHeader = httpContext.Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith(Authentication.Basic.BasicDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }

            //var userContext = httpContext.RequestServices.GetService<UserContext>();
            var codes = httpContext.GetEndpoint().Metadata?.GetMetadata<ContelWorksAuthorizeAttribute>()?.Codes;
            if (codes.Any() != true)
            {
                context.Succeed(requirement);
                return;
            }

            //var result = await CheckUserPermissions(userContext.Id, codes, userContext.RoleIds);
            //if (result)
            //{
            //    context.Succeed(requirement);
            //    return;
            //}
        }
        context.Fail();
    }

    protected abstract Task<bool> CheckUserPermissions(long userId, IEnumerable<string> requestPermissions, string userBelongsRoleIds);
}