using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Maintenance.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl.Maintenance
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [Route("maint")]
    [Authorize]
    public class LogController : ContelWorksControllerBase
    {
        private readonly ILogAppService _logService;

        public LogController(ILogAppService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// 查询操作日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("opslogs")]
        [ContelWorksAuthorize(PermissionConsts.Log.GetListForOperationLog)]
        public async Task<PageModelDto<OpsLogDto>> GetOpsLogsPaged([FromQuery] LogSearchPagedDto searchDto)
            => await _logService.GetOpsLogsPagedAsync(searchDto);

        /// <summary>
        /// 查询用户操作日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("users/opslogs")]
        public async Task<PageModelDto<OpsLogDto>> GetUserOpsLogsPagedAsync([FromQuery] LogSearchPagedDto searchDto)
        {
            //var logSearchDto = new LogSearchPagedDto()
            //{
            //    Account = App.UserId.ToString(),
            //    PageIndex = searchDto.PageIndex,
            //    PageSize = searchDto.PageSize
            //};
            return await _logService.GetOpsLogsPagedAsync(searchDto);
        }

        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("loginlogs")]
        [ContelWorksAuthorize(PermissionConsts.Log.GetListForLogingLog)]
        public async Task<PageModelDto<LoginLogDto>> GetLoginLogsPagedAsync([FromQuery] LogSearchPagedDto searchDto)
            => await _logService.GetLoginLogsPagedAsync(searchDto);

        /// <summary>
        /// 查询Nlog日志
        /// </summary>
        /// <param name="searchDto">查询条件</param>
        /// <returns></returns>
        [HttpGet("nloglogs")]
        [ContelWorksAuthorize(PermissionConsts.Log.GetListForNLog)]
        public async Task<PageModelDto<NlogLogDto>> GetNlogLogsPagedAsync([FromQuery] LogSearchPagedDto searchDto)
            => await _logService.GetNlogLogsPagedAsync(searchDto);
    }
}