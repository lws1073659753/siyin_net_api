using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace SiyinPractice.Web.Core.Authentication.Basic;

public class BasicTokenValidatedContext : ResultContext<BasicSchemeOptions>
{
    public BasicTokenValidatedContext(HttpContext context, AuthenticationScheme scheme, BasicSchemeOptions options)
        : base(context, scheme, options)
    {
    }
}