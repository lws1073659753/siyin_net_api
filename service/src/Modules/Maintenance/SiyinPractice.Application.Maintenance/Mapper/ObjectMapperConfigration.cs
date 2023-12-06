using SiyinPractice.Domain.Maintenance;
using SiyinPractice.Framework.Mapper;
using SiyinPractice.Shared.Maintenance.Dto;

namespace SiyinPractice.Application.Maintenance.Mapper
{
    public class ObjectMapperConfigration : IObjectMapperConfigration
    {
        public IList<ObjectMapperCreater> ObjectMapperCreaterBuilder()
        {
            var mappingData = new List<ObjectMapperCreater>();
            mappingData.Add(new ObjectMapperCreater<OpsLogCreationDto, OperationLog>());
            mappingData.Add(new ObjectMapperCreater<OperationLog, OpsLogDto>());
            mappingData.Add(new ObjectMapperCreater<LoginLog, LoginLogDto>());
            mappingData.Add(new ObjectMapperCreater<LoggerLog, NlogLogDto>());
            mappingData.Add(new ObjectMapperCreater<SysNloglogProperty, NlogLogPropertyDto>());
            mappingData.Add(new ObjectMapperCreater<CfgCreationDto, SysCfg>());
            mappingData.Add(new ObjectMapperCreater<SysCfg, CfgDto>());
            mappingData.Add(new ObjectMapperCreater<DictCreationDto, SysDict>());
            mappingData.Add(new ObjectMapperCreater<SysDict, DictDto>());
            mappingData.Add(new ObjectMapperCreater<SysNotice, NoticeDto>().ReverseMap());
            return mappingData;
        }
    }
}