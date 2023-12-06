using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace SiyinPractice.Web.Core.Authentication.Bearer;

public class BearerTokenValidatedContext : ResultContext<BearerSchemeOptions>
{
    public BearerTokenValidatedContext(HttpContext context, AuthenticationScheme scheme, BearerSchemeOptions options)
        : base(context, scheme, options)
    {
    }
}