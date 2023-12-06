using SiyinPractice.Application.Core;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework.Security;
using SiyinPractice.Infrastructure.EntityFramework;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Maintenance.Dto;
using System.Linq.Expressions;

namespace Adnc.Maint.Application.Services;

public class NoticeAppService : AppService, INoticeAppService
{
    private readonly IEfRepository<SysNotice> _noticeRepository;
    private IContextService _contextService;

    public NoticeAppService(IEfRepository<SysNotice> noticeRepository, IContextService contextService)
    {
        _noticeRepository = noticeRepository;
        _contextService = contextService;
    }

    public async Task<List<NoticeDto>> GetListAsync(NoticeSearchDto search)
    {
        var whereCondition = ExpressionCreator
                                            .New<SysNotice>()
                                            .AndIf(search.Title.IsNotNullOrWhiteSpace(), x => x.Title == search.Title.Trim());

        var notices = _noticeRepository
                                        .Where(whereCondition)
                                        .ToList();

        return Mapper.Map<List<NoticeDto>>(notices);
    }
    public async Task<int> AddClearData()
    {
        var sql = "exec BackupAndDeleteTable";
        NoticeDto noticeDto = new NoticeDto();
        noticeDto.Creator = UserTokenService.GetUserToken().UserName;
        noticeDto.CreateTime =DateTime.Now;
        noticeDto.Content ="清除数据";
        noticeDto.Title ="清除数据";
        var notice= Mapper.Map<SysNotice>(noticeDto);
         await _noticeRepository.InsertAsync(notice);
        return await _contextService.ExecuteNonQuery(sql,null);
    }
}