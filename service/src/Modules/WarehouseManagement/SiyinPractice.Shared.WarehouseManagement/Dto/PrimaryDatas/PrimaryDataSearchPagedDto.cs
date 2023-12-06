using ConnmIntel.Shared.Core.Dto;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.PrimaryDatas
{
    public class PrimaryDataSearchPagedDto : NamedSearchPagedDto
    {
        public  string PiNum { get; set; }   // 实物bin
        public  string SysBin { get; set; }   // 实物bin
        public  string SysSn { get; set; }   // 实物sn
        public  string SysPn { get; set; }   // 实物pn
        public  string BeginTime { get; set; }   //开始时间
        public  string EndTime { get; set; }   // 结束时间
    }
}