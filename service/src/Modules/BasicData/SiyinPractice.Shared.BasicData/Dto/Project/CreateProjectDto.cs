using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiyinPractice.Shared.BasicData.Dto.Project
{
    public class CreateProjectDto : CreateAuditEntityDto
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
        /// 部门
        /// </summary>
        [Description("部门")]
        public string? CreateDept { get; set; }
    }
}
