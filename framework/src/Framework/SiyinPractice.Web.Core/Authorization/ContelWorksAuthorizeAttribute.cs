using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SiyinPractice.Web.Core.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class ContelWorksAuthorizeAttribute : AuthorizeAttribute
{
    public const string JwtWithBasicSchemes = $"{JwtBearerDefaults.AuthenticationScheme},{Authentication.Basic.BasicDefaults.AuthenticationScheme}";

    public string[] Codes { get; set; }

    public ContelWorksAuthorizeAttribute(string code, string schemes = JwtBearerDefaults.AuthenticationScheme)
        : this(new string[] { code }, schemes)
    {
    }

    public ContelWorksAuthorizeAttribute(string[] codes, string schemes = JwtBearerDefaults.AuthenticationScheme)
    {
        Codes = codes;
        //Policy = AuthorizePolicy.Default;
        if (schemes.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(schemes));
        else
            AuthenticationSchemes = schemes;
    }
}