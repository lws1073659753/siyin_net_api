using SiyinPractice.Application.Core;
using SiyinPractice.Domain.Core;
using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Interface.Maintenance;
using SiyinPractice.Shared.Core.Dto;
using SiyinPractice.Shared.Core.Exceptions;
using SiyinPractice.Shared.Maintenance.Dto;
using System.Linq.Expressions;

namespace Adnc.Maint.Application.Services;

public class LogAppService : AppService, ILogAppService
{
    private readonly IEfRepository<OperationLog> _opsLogRepository;
    private readonly IEfRepository<LoggerLog> _nlogLogRepository;
    private readonly IEfRepository<LoginLog> _loginLogRepository;

    public LogAppService(IEfRepository<OperationLog> opsLogRepository
        , IEfRepository<LoginLog> loginLogRepository
        , IEfRepository<LoggerLog> nlogLogRepository)
    {
        _opsLogRepository = opsLogRepository;
        _loginLogRepository = loginLogRepository;
        _nlogLogRepository = nlogLogRepository;
    }

    public async Task<PageModelDto<LoginLogDto>> GetLoginLogsPagedAsync(LogSearchPagedDto searchDto)
    {
        //var builder = Builders<LoginLog>.Filter;
        //var filterList = new List<FilterDefinition<LoginLog>>();
        //filterList.AddIf(x => searchDto.BeginTime.HasValue, builder.Gte(l => l.CreateTime, searchDto.BeginTime));
        //filterList.AddIf(x => searchDto.EndTime.HasValue, builder.Lte(l => l.CreateTime, searchDto.EndTime));
        //filterList.AddIf(x => searchDto.Account.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Account, searchDto.Account));
        //filterList.AddIf(x => searchDto.Method.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Device, searchDto.Device));
        //var filterDefinition = filterList.IsNotNullOrEmpty() ? builder.And(filterList) : builder.Where(x => true);

        //var pagedModel = await _loginLogRepository.PagedAsync(searchDto.PageIndex, searchDto.PageSize, filterDefinition, x => x.CreateTime, false);
        //var result = Mapper.Map<PageModelDto<LoginLogDto>>(pagedModel);

        //return result;

        var whereExpression = ExpressionCreator
                             .New<LoginLog>()
                             .AndIf(searchDto.BeginTime.HasValue, x => x.CreateTime > searchDto.BeginTime)
                             .AndIf(searchDto.EndTime.HasValue, x => x.CreateTime < searchDto.EndTime)
                             .AndIf(searchDto.Account.IsNotNullOrWhiteSpace(), x => x.Account == searchDto.Account)
                             .AndIf(searchDto.Method.IsNotNullOrWhiteSpace(), x => x.Device == searchDto.Device);

        var total = await _loginLogRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<LoginLogDto>(searchDto);

        var entities = _loginLogRepository
                                     .Where(whereExpression)
                                     .OrderByDescending(x => x.CreateTime)
                                     .Skip(searchDto.SkipRows())
                                     .Take(searchDto.PageSize)
                                     .ToList();
        var loginLogDtos = Mapper.Map<List<LoginLogDto>>(entities);

        return new PageModelDto<LoginLogDto>(searchDto, loginLogDtos, total);
    }

    public async Task<PageModelDto<OpsLogDto>> GetOpsLogsPagedAsync(LogSearchPagedDto searchDto)
    {
        //var builder = Builders<OperationLog>.Filter;
        //var filterList = new List<FilterDefinition<OperationLog>>();
        //filterList.AddIf(x => searchDto.BeginTime.HasValue, builder.Gte(l => l.CreateTime, searchDto.BeginTime));
        //filterList.AddIf(x => searchDto.EndTime.HasValue, builder.Lte(l => l.CreateTime, searchDto.EndTime));
        //filterList.AddIf(x => searchDto.Account.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Account, searchDto.Account));
        //filterList.AddIf(x => searchDto.Method.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Method, searchDto.Method));
        //var filterDefinition = filterList.IsNotNullOrEmpty() ? builder.And(filterList) : builder.Where(x => true);

        //var pagedModel = await _opsLogRepository.PagedAsync(searchDto.PageIndex, searchDto.PageSize, filterDefinition, x => x.CreateTime, false);
        //var result = Mapper.Map<PageModelDto<OpsLogDto>>(pagedModel);

        //return result;

        var whereExpression = ExpressionCreator
                            .New<OperationLog>()
                            .AndIf(searchDto.BeginTime.HasValue, x => x.CreateTime > searchDto.BeginTime)
                            .AndIf(searchDto.EndTime.HasValue, x => x.CreateTime < searchDto.EndTime)
                            .AndIf(searchDto.Account.IsNotNullOrWhiteSpace(), x => x.Account == searchDto.Account)
                            .AndIf(searchDto.Method.IsNotNullOrWhiteSpace(), x => x.Method == searchDto.Method);

        var total = await _opsLogRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<OpsLogDto>(searchDto);

        var entities = _opsLogRepository
                                     .Where(whereExpression)
                                     .OrderByDescending(x => x.CreateTime)
                                     .Skip(searchDto.SkipRows())
                                     .Take(searchDto.PageSize)
                                     .ToList();
        var loginLogDtos = Mapper.Map<List<OpsLogDto>>(entities);

        return new PageModelDto<OpsLogDto>(searchDto, loginLogDtos, total);
    }

    public async Task<PageModelDto<NlogLogDto>> GetNlogLogsPagedAsync(LogSearchPagedDto searchDto)
    {
        //var builder = Builders<LoggerLog>.Filter;
        //var filterList = new List<FilterDefinition<LoggerLog>>();
        //filterList.AddIf(x => searchDto.BeginTime.HasValue, builder.Gte(l => l.Date, searchDto.BeginTime));
        //filterList.AddIf(x => searchDto.EndTime.HasValue, builder.Lte(l => l.Date, searchDto.EndTime));
        //filterList.AddIf(x => searchDto.Method.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Properties.Method, searchDto.Method));
        //filterList.AddIf(x => searchDto.Level.IsNotNullOrWhiteSpace(), builder.Eq(l => l.Level, searchDto.Level));
        //var filterDefinition = filterList.Count > 0 ? builder.And(filterList) : builder.Where(x => true);

        //var pagedModel = await _nlogLogRepository.PagedAsync(searchDto.PageIndex, searchDto.PageSize, filterDefinition, x => x.Date, false);
        //var result = Mapper.Map<PageModelDto<NlogLogDto>>(pagedModel);

        //return result;

        var whereExpression = ExpressionCreator
                          .New<LoggerLog>()
                          .AndIf(searchDto.BeginTime.HasValue, x => x.Date > searchDto.BeginTime)
                          .AndIf(searchDto.EndTime.HasValue, x => x.Date < searchDto.EndTime)
                          .AndIf(searchDto.Method.IsNotNullOrWhiteSpace(), x => x.Properties.Method == searchDto.Method)
                          .AndIf(searchDto.Account.IsNotNullOrWhiteSpace(), x => x.Level == searchDto.Level);

        var total = await _nlogLogRepository.CountAsync(whereExpression);
        if (total == 0)
            return new PageModelDto<NlogLogDto>(searchDto);

        var entities = _nlogLogRepository
                                     .Where(whereExpression)
                                     .OrderByDescending(x => x.Date)
                                     .Skip(searchDto.SkipRows())
                                     .Take(searchDto.PageSize)
                                     .ToList();
        var loginLogDtos = Mapper.Map<List<NlogLogDto>>(entities);

        return new PageModelDto<NlogLogDto>(searchDto, loginLogDtos, total);
    }
}