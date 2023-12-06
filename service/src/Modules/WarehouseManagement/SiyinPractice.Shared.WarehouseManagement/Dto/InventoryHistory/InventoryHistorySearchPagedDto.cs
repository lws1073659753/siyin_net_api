using ConnmIntel.Shared.Core.Dto;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory
{
    public class InventoryHistorySearchPagedDto : NamedSearchPagedDto
    {
        public string DeptName { get; set; }   // BoxName
        public string ProjectName { get; set; }   // BoxName
        public string Location { get; set; }   // BoxName
        public string BoxName { get; set; }   // BoxName
        public string Pallet { get; set; }   // BoxName
        public string SysBin { get; set; }   // 实物bin
        public string ScanSn { get; set; }   // 实物sn
        public string ScanPn { get; set; }   // 实物pn
        public string Creator { get; set; }   // 实物pn
        public string BeginTime { get; set; }   //开始时间
        public string EndTime { get; set; }   // 结束时间 
    }
}