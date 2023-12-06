using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class UserDto : AuditEntityDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public Guid? DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色Id列表，以逗号分隔
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 性别描述
        /// </summary>
        public string SexName
        {
            get
            {
                string result = "未知";
                if (this.Sex.HasValue)
                {
                    result = this.Sex.Value == 1 ? "男" : "女";
                }

                return result;
            }
        }

        /// <summary>
        /// 账户状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 钉钉号
        /// </summary>
        public string DingDing { get; set; }

        /// <summary>
        /// 账户状态描述
        /// </summary>
        public string StatusName => this.Status == 1 ? "启用" : "禁用";

        ///// <summary>
        ///// 账户版本号
        ///// </summary>
        //public int? Version { get; set; }
    }
}