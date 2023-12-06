using SiyinPractice.Shared.Core.Dto;
using System.ComponentModel;

namespace SiyinPractice.Shared.BasicData.Dto.Project
{
    public class ProjectDto : AuditEntityDto
    {
        /// <summary>
        /// box前缀
        /// </summary>
        [Description("box前缀")]
        public string? BoxPrefix { get; set; }

        /// <summary>
        /// TAG_NO前缀
        /// </summary>
        [Description("TAG_NO前缀")]
        public string? TAG_NOPrefix { get; set; }

        /// <summary>
        /// 条码替换
        /// </summary>
        [Description("条码替换")]
        public string? SnReplace { get; set; }

        /// <summary>
        /// 末尾屏蔽
        /// </summary>
        [Description("末尾屏蔽")]
        public string? EndShield { get; set; }

        /// <summary>
        /// 是否厂内sn
        /// </summary>
        [Description("是否厂内sn")]
        public bool? IsPlantSn { get; set; }
        /// <summary>
        /// 条码替换
        /// </summary>
        [Description("条码替换")]
        public string? CreateDept { get; set; }
    }
}