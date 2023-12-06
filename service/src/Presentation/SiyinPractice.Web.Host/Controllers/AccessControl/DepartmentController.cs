using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.AccessControl.Dto.Dept;
using SiyinPractice.Web.Core.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Web.Host.Controllers.AccessControl
{
    /// <summary>
    /// 部门
    /// </summary>
    [Route("usr/depts")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ContelWorksControllerBase
    {
        private readonly IDepartmentAppService _deptService;

        public DepartmentController(IDepartmentAppService deptService)
           => _deptService = deptService;

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Dept.Delete)]
        public async Task<int> Delete([FromRoute] Guid id)
            => await _deptService.DeleteAsync(id);

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="input">部门</param>
        /// <returns></returns>
        [HttpPost]
        [ContelWorksAuthorize(PermissionConsts.Dept.Create)]
        public async Task<Guid> CreateAsync([FromBody] CreateDepartmentDto input)
            => await _deptService.CreateAsync(input);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">部门</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ContelWorksAuthorize(PermissionConsts.Dept.Update)]
        public async Task<int> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateDepartmentDto input)
            => await _deptService.UpdateAsync(id, input);

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ContelWorksAuthorize(PermissionConsts.Dept.GetList, ContelWorksAuthorizeAttribute.JwtWithBasicSchemes)]
        public async Task<ActionResult<List<DepartmentTreeDto>>> GetListAsync()
            => await _deptService.GetTreeListAsync();

        /// <summary>
        /// 获取部门all
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public List<DeptDto> GetAll()
            => _deptService.GetAll();
    }
}