using SiyinPractice.Interface.Core;
using SiyinPractice.Shared.Maintenance.Dto;

namespace SiyinPractice.Interface.Maintenance;

/// <summary>
/// 通知管理
/// </summary>
public interface INoticeAppService : IAppService
{
    /// <summary>
    /// 获取通知列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    Task<List<NoticeDto>> GetListAsync(NoticeSearchDto search);
    Task<int> AddClearData();
}