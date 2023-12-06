using SiyinPractice.Domain.Business;
using System.ComponentModel;

namespace SiyinPractice.Domain.BasicData
{
    /// <summary>
    /// 项目
    /// </summary>
    [Description("项目")]
    public class Project : BusinessEntity
    {
        /// <summary>
        /// box前缀
        /// </summary>
        [Description("box前缀")]
        public virtual string? BoxPrefix { get; set; }
        /// <summary>
        /// TAG_NO前缀
        /// </summary>
        [Description("TAG_NO前缀")]
        public virtual string? TAG_NOPrefix { get; set; }
        /// <summary>
        /// 条码替换
        /// </summary>
        [Description("条码替换")]
        public virtual string? SnReplace { get; set; }
        /// <summary>
        /// 末尾屏蔽
        /// </summary>
        [Description("末尾屏蔽")]
        public virtual string? EndShield { get; set; }
        /// <summary>
        /// 是否厂内sn
        /// </summary>
        [Description("是否厂内sn")]
        public virtual bool? IsPlantSn { get; set; }
        /// <summary>
        /// 导入部门
        /// </summary>
        [Description("导入部门")]
        public virtual string? CreateDept { get; set; }
    }
}