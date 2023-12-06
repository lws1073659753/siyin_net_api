using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Maintenance.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl.Maintenance
{
    /// <summary>
    /// 配置管理
    /// </summary>
    [Route("maint/cfgs")]
    [ApiController]
    [Authorize]
    public class ConfigController : ContelWorksControllerBase
    {
        private readonly IConfigAppService _cfgAppService;

        public ConfigController(IConfigAppService cfgAppService) => _cfgAppService = cfgAppService;

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="input"><see cref="CfgCreationDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.Cfg.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Guid> CreateAsync([FromBody] CfgCreationDto input) =>
            await _cfgAppService.CreateAsync(input);

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input"><see cref="CfgUpdationDto"/></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Cfg.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] CfgUpdationDto input) =>
            await _cfgAppService.UpdateAsync(id, input);

        /// <summary>
        /// 删除配置节点
        /// </summary>
        /// <param name="id">节点id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Cfg.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<int> DeleteAsync([FromRoute] Guid id) =>
            await _cfgAppService.DeleteAsync(id);

        /// <summary>
        /// 获取单个配置节点
        /// </summary>
        /// <param name="id">节点id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Cfg.GetList, ContelWorksAuthorizeAttribute.JwtWithBasicSchemes)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CfgDto>> GetAsync([FromRoute] Guid id) =>
           await _cfgAppService.GetAsync(id);

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="search"><see cref="CfgSearchPagedDto"/></param>
        /// <returns><see cref="PageModelDto{CfgDto}"/></returns>
        [HttpGet()]
        //[AdncAuthorize(PermissionConsts.Cfg.GetList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageModelDto<CfgDto>>> GetPagedAsync([FromQuery] CfgSearchPagedDto search) =>
            await _cfgAppService.GetPagedAsync(search);
    }
}