using SiyinPractice.Framework.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationExtension
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfigurationSection = configuration.GetSection(JwtConfig.Name);
            services.Configure<JwtConfig>(jwtConfigurationSection);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   var bearerConfig = jwtConfigurationSection.Get<JwtConfig>();
                   //options.TokenValidationParameters = JwtSecurityTokenHandlerExtension.GenarateTokenValidationParameters(bearerConfig);

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = bearerConfig.ValidateIssuer,
                       ValidIssuer = bearerConfig.ValidIssuer,
                       ValidateIssuerSigningKey = bearerConfig.ValidateIssuerSigningKey,
                       IssuerSigningKey = new SymmetricSecurityKey(bearerConfig.Encoding.GetBytes(bearerConfig.SymmetricSecurityKey)),
                       ValidateAudience = bearerConfig.ValidateAudience,
                       ValidAudience = bearerConfig.ValidAudience,
                       ValidateLifetime = bearerConfig.ValidateLifetime,
                       RequireExpirationTime = bearerConfig.RequireExpirationTime,
                       ClockSkew = TimeSpan.FromSeconds(bearerConfig.ClockSkew),
                   };

                   //options.SecurityTokenValidators.Clear();
                   //options.SecurityTokenValidators.Add(securityTokenHandler);
               })
               .AddBasic(options => options.Events.OnTokenValidated = (context) =>
               {
                   //var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                   //var claims = context.Principal.Claims;
                   //userContext.Id = long.Parse(claims.First(x => x.Type == BasicDefaults.NameId).Value);
                   //userContext.Account = claims.First(x => x.Type == BasicDefaults.UniqueName).Value;
                   //userContext.Name = claims.First(x => x.Type == BasicDefaults.Name).Value;
                   //userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                   return Task.CompletedTask;
               });
            //services.AddAuthorization();
        }
    }
}