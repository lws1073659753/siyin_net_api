using ConnmIntel.Shared.Core.Dto;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.AtchNo
{
    public class AtchNoDto : AuditEntityDto
    {    /// <summary>
         /// 批次号状态
         /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 是否自动生成
        /// </summary>
        public bool AutomaticGeneration { get; set; }
        /// <summary>
        /// 总盘点量
        /// </summary>
        public int TotalInventory { get; set; }
        /// <summary>
        /// 待盘数量
        /// </summary>
        public int ToBeCounted { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public  string DeptName { get; set; }
    }
}