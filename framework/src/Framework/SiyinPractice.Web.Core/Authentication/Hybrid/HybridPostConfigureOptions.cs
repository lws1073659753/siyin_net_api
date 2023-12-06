using Microsoft.Extensions.Options;

namespace SiyinPractice.Web.Core.Authentication.Hybrid;

public class HybridPostConfigureOptions : IPostConfigureOptions<HybridSchemeOptions>
{
    public void PostConfigure(string name, HybridSchemeOptions options)
    {
        // Method intentionally left empty.
    }
}