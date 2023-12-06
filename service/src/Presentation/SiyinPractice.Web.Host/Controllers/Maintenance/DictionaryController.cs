using SiyinPractice.Domain.Shared.Maintenance;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Maintenance.Dto;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl.Maintenance
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Route("maint/dicts")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ContelWorksControllerBase
    {
        private readonly IDictionaryAppService _dictAppService;

        public DictionaryController(IDictionaryAppService dictAppService) =>
            _dictAppService = dictAppService;

        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="input"><see cref="DictCreationDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.Dict.Create)]
        public async Task<Guid> CreateAsync([FromBody] DictCreationDto input) =>
            await _dictAppService.CreateAsync(input);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input"><see cref="DictUpdationDto"/></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Dict.Update)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] DictUpdationDto input) =>
            await _dictAppService.UpdateAsync(id, input);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id">字典ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Dict.Delete)]
        public async Task<int> DeleteAsync([FromRoute] Guid id) =>
            await _dictAppService.DeleteAsync(id);

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ContelWorksAuthorize(PermissionConsts.Dict.GetList)]
        public async Task<List<DictDto>> GetListAsync([FromQuery] DictSearchDto search) =>
            await _dictAppService.GetListAsync(search);

        /// <summary>
        /// 获取单个字典数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Dict.GetList, ContelWorksAuthorizeAttribute.JwtWithBasicSchemes)]
        public async Task<ActionResult<DictDto>> GetAsync([FromRoute] Guid id)
        {
            var dict = await _dictAppService.GetAsync(id);
            if (dict is not null)
                return dict;

            return NotFound();
        }
    }
}