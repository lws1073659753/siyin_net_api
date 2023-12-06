using ConnmIntel.Shared.Core.Dto;
using System.ComponentModel;

namespace ConnmIntel.Shared.WarehouseManagement.Dto.InventoryHistory
{
    public class InventoryHistoryDto : AuditEntityDto
    {
        /// <summary>
        /// 实物sn
        /// </summary>
        [Description("实物sn")]
        public string ScanSn { get; set; }   // 实物sn

        /// <summary>
        /// 实物Pn
        /// </summary>
        [Description("实物Pn")]
        public string ScanPn { get; set; }   // 实物Pn

        /// <summary>
        /// 批次号
        /// </summary>
        [Description("批次号")]
        public string PiNum { get; set; }   // 批次号

        /// <summary>
        /// Sn部门
        /// </summary>
        [Description("Sn部门")]
        public string PiDpt { get; set; }   // Sn部门

        /// <summary>
        /// 项目号
        /// </summary>
        [Description("项目号")]
        public string PiProject { get; set; }   // 项目号

        /// <summary>
        /// 希捷sn
        /// </summary>
        [Description("希捷sn")]
        public string SysOrgSn { get; set; }   // 希捷sn

        /// <summary>
        /// 希捷sys_org_pn
        /// </summary>
        [Description("希捷sys_org_pn")]
        public string SysOrgPn { get; set; }   // 希捷sys_org_pn
        /// <summary>
        /// 导入sn
        /// </summary>
        [Description("导入sn")]
        public string SysSn { get; set; }   // 导入sn
        /// <summary>
        /// 导入pn
        /// </summary>
        [Description("导入pn")]
        public string SysPn { get; set; }   // 导入pn

        /// <summary>
        /// 货位
        /// </summary>
        [Description("货位")]
        public string SysBin { get; set; }   // 货位

        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public string SysLocation { get; set; }   // 区域

        /// <summary>
        /// 来源
        /// </summary>
        [Description("来源")]
        public string Source { get; set; }   // 来源

        /// <summary>
        /// 账册编号
        /// </summary>
        [Description("账册编号")]
        public string AccountBook { get; set; }   // 账册编号

        /// <summary>
        /// 备案编号
        /// </summary>
        [Description("备案编号")]
        public string FilingNo { get; set; }   // 备案编号

        /// <summary>
        /// 导入部门
        /// </summary>
        [Description("导入部门")]
        public string CreateDept { get; set; }   // 导入部门

        /// <summary>
        /// 扫入托盘
        /// </summary>
        [Description("扫入托盘")]
        public string ScanPallet { get; set; }   // 扫入托盘

        /// <summary>
        /// 自动tag
        /// </summary>
        [Description("自动tag")]
        public string AutomaticTag { get; set; }   // 自动tag

        /// <summary>
        /// box
        /// </summary>
        [Description("Box")]
        public string BoxName { get; set; }   // box
        /// <summary>
        /// 扫入sn状态
        /// </summary>
        [Description("扫入sn状态")]
        public string SnState { get; set; }   // 扫入Sn状态
        /// <summary>
        /// 扫入Pn状态
        /// </summary>
        [Description("扫入Pn状态")]
        public string PnState { get; set; }   // 扫入Pn状态
        public string SnRules { get; set; } //sn规则
        public string PnRules { get; set; }//pn规则
        public string SnReplace { get; set; }  //sn替换
        public string EndShield { get; set; }//末尾屏蔽
        public string BoxPrefix { get; set; } //box前缀
        public string TagPrefix { get; set; } //tag前缀
        public bool IsPlantSn { get; set; } //是否厂内sn
        /// <summary>
        /// 项目号
        /// </summary>
        [Description("项目号")]
        public string ProjectName { get; set; }   // 项目号
    }
}