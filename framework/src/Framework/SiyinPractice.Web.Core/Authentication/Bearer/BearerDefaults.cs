using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SiyinPractice.Web.Core.Authentication.Bearer;

public static class BearerDefaults
{
    public const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
    public const string RoleIds = "roleids";
}