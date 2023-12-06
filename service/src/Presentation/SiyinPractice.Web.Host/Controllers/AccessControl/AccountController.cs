using SiyinPractice.Framework.Configuration;
using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Web.Core.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl
{
    /// <summary>
    /// 认证服务
    /// </summary>
    [Route("auth/session")]
    [ApiController]
    [Authorize]
    public class AccountController : ContelWorksControllerBase
    {
        private readonly IOptions<JwtConfig> _jwtOptions;

        private readonly IAccountAppService _accountService;

        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IOptions<JwtConfig> jwtOptions,
            IAccountAppService accountService,
            ILogger<AccountController> logger
            )
        {
            _jwtOptions = jwtOptions;
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"><see cref="UserLoginDto"/></param>
        /// <returns><see cref="UserTokenInfoDto"></see>/></returns>
        [AllowAnonymous]
        [HttpPost()]
        public async Task<UserTokenInfoDto> LoginAsync([FromBody] UserLoginDto input)
        {
            var validatedInfo = await _accountService.LoginAsync(input);
            var accessToken = JwtTokenHelper.CreateAccessToken(_jwtOptions.Value, validatedInfo.ValidationVersion, validatedInfo.Account, validatedInfo.Id.ToString(), validatedInfo.Name, validatedInfo.RoleIds, JwtBearerDefaults.Manager);
            var refreshToken = JwtTokenHelper.CreateRefreshToken(_jwtOptions.Value, validatedInfo.ValidationVersion, validatedInfo.Id.ToString());
            var tokenInfo = new UserTokenInfoDto(accessToken.Token, accessToken.Expire, refreshToken.Token, refreshToken.Expire);
            return tokenInfo;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public async Task LogoutAsync() => await _accountService.DeleteUserValidateInfoAsync(UserTokenService.GetUserToken().UserId);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="input"><see cref="UserRefreshTokenDto"/></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPut()]
        public async Task<ActionResult<UserTokenInfoDto>> RefreshAccessTokenAsync([FromBody] UserRefreshTokenDto input)
        {
            var claimOfId = JwtTokenHelper.GetClaimFromRefeshToken(_jwtOptions.Value, input.RefreshToken, JwtRegisteredClaimNames.NameId);
            if (claimOfId is not null)
            {
                //var id = Guid.Parse(claimOfId.Value);
                var isGuid = Guid.TryParse(claimOfId.Value, out Guid id);
                if (!isGuid)
                    return Forbid();

                var validatedInfo = await _accountService.GetUserValidatedInfoAsync(id);
                if (validatedInfo is null)
                    return Forbid();

                var jti = JwtTokenHelper.GetClaimFromRefeshToken(_jwtOptions.Value, input.RefreshToken, JwtRegisteredClaimNames.Jti);
                if (jti.Value != validatedInfo.ValidationVersion)
                    return Forbid();

                var accessToken = JwtTokenHelper.CreateAccessToken(_jwtOptions.Value, validatedInfo.ValidationVersion, validatedInfo.Account, validatedInfo.Id.ToString(), validatedInfo.Name, validatedInfo.RoleIds, JwtBearerDefaults.Manager);
                var refreshToken = JwtTokenHelper.CreateRefreshToken(_jwtOptions.Value, validatedInfo.ValidationVersion, validatedInfo.Id.ToString());

                await _accountService.ChangeUserValidateInfoExpiresDtAsync(id);

                var tokenInfo = new UserTokenInfoDto(accessToken.Token, accessToken.Expire, refreshToken.Token, refreshToken.Expire);
                return Ok(tokenInfo);
            }
            return Forbid();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"><see cref="ChangeUserPwdDto"/></param>
        /// <returns></returns>
        [HttpPut("password")]
        public async Task ChangePassword([FromBody] ChangeUserPwdDto input) => await _accountService.UpdatePasswordAsync(UserTokenService.GetUserToken().UserId, input);

        /// <summary>
        ///  获取认证信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<UserValidatedInfoDto>> GetUserValidatedInfoAsync()
        {
            var result = await _accountService.GetUserValidatedInfoAsync(UserTokenService.GetUserToken().UserId);
            _logger.LogDebug($"UserContext:{UserTokenService.GetUserToken().UserId}");
            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}