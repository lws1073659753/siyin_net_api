using SiyinPractice.Framework.Security;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Maintenance.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl.Maintenance
{
    /// <summary>
    /// 通知管理
    /// </summary>
    [Route("maint/notices")]
    [ApiController]
    [Authorize]
    public class NoticeController : ContelWorksControllerBase
    {
        private readonly INoticeAppService _noticeService;

        public NoticeController(INoticeAppService noticeService)
        {
            _noticeService = noticeService;
        }

        /// <summary>
        /// 获取通知消息列表
        /// </summary>
        /// <param name="search"><see cref="NoticeSearchDto"/></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet()]
        public async Task<List<NoticeDto>> GetList([FromQuery] NoticeSearchDto search)
        {
            if (UserTokenService.GetUserToken().UserId == Guid.Empty)
                return await Task.FromResult(new List<NoticeDto>());
            else
                return await _noticeService.GetListAsync(search);
        }
        [HttpGet]
        [Route("AddClearData")]
        public Task<int> AddClearData()
        {
            return _noticeService.AddClearData();
        }
    }
}