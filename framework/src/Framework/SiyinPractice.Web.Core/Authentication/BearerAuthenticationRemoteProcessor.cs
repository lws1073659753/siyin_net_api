using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SiyinPractice.Web.Core.Authentication;

public class BearerAuthenticationRemoteProcessor : AbstractAuthenticationProcessor
{
    private IHttpContextAccessor _contextAccessor;
    private ILogger<BearerAuthenticationRemoteProcessor> _logger;

    public BearerAuthenticationRemoteProcessor(
        IHttpContextAccessor contextAccessor,
        ILogger<BearerAuthenticationRemoteProcessor> logger
        )
    {
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    protected override Task<(string ValidationVersion, int Status)> GetValidatedInfoAsync(long userId)
    {
        throw new NotImplementedException();
    }

    //protected override async Task<(string ValidationVersion, int Status)> GetValidatedInfoAsync(long userId)
    //{
    //var userContext = _contextAccessor.HttpContext.RequestServices.GetService<UserContext>();
    //userContext.ExationId = userId;

    //var apiReuslt = await _authRestClient.GetValidatedInfoAsync();
    //if (!apiReuslt.IsSuccessStatusCode)
    //{
    //    var message = $"{apiReuslt.StatusCode}:{apiReuslt.Error?.Message}";
    //    _logger.LogDebug(message);
    //    return (null, 0);
    //}

    //return (apiReuslt.Content.ValidationVersion, apiReuslt.Content.Status);

    //}
}