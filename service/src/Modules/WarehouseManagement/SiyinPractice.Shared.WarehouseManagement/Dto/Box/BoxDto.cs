using ConnmIntel.Shared.Core.Dto;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.Box
{
    public class BoxDto : AuditEntityDto
    {
        /// <summary>
        /// Box状态
        /// </summary>
        public string State { get; set; }
        ///name ===boxname
        /// <summary>
        /// bin
        /// </summary>c string PiNum { get; set; }     //批次号
        public string Bin { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 是否自动生成
        /// </summary>
        public bool AutomaticGeneration { get; set; }
        /// <summary>
        /// box数量
        /// </summary>
        public int BoxCount { get; set; }
        /// <summary>
        /// paller拖号
        /// </summary>
        public  string Pallet { get; set; }
    }
}