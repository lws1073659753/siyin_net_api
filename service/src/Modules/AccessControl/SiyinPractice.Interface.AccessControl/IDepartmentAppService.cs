using SiyinPractice.Caching.Interceptor;
using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Interface.Core;
using SiyinPractice.Interface.Core.Attributes;
using SiyinPractice.Shared.AccessControl.Dto;
using SiyinPractice.Shared.AccessControl.Dto.Dept;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiyinPractice.Interface.AccessControl
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDepartmentAppService : IAppService
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "新增部门")]
        [CachingEvict(CacheKeys = new string[] { CachingConsts.DetpListCacheKey, CachingConsts.DetpTreeListCacheKey, CachingConsts.DetpSimpleTreeListCacheKey })]
        Task<Guid> CreateAsync(CreateDepartmentDto input);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [OperateLog(LogName = "修改部门")]
        [CachingEvict(CacheKeys = new string[] { CachingConsts.DetpListCacheKey, CachingConsts.DetpTreeListCacheKey, CachingConsts.DetpSimpleTreeListCacheKey })]
        Task<int> UpdateAsync(Guid id, UpdateDepartmentDto input);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperateLog(LogName = "删除部门")]
        [CachingEvict(CacheKeys = new string[] { CachingConsts.DetpListCacheKey, CachingConsts.DetpTreeListCacheKey, CachingConsts.DetpSimpleTreeListCacheKey })]
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// 部门树结构
        /// </summary>
        /// <returns></returns>
        [CachingAble(CacheKey = CachingConsts.DetpTreeListCacheKey, Expiration = CachingConsts.OneYear)]
        Task<List<DepartmentTreeDto>> GetTreeListAsync();

        //[CachingAble(CacheKey = CachingConsts.DeptGetAll, Expiration = CachingConsts.OneYear)]
        List<DeptDto> GetAll();
    }
}