using Microsoft.Extensions.Options;

namespace SiyinPractice.Web.Core.Authentication.Basic;

public class BasicPostConfigureOptions : IPostConfigureOptions<BasicSchemeOptions>
{
    public void PostConfigure(string name, BasicSchemeOptions options)
    {
        // Method intentionally left empty.
    }
}