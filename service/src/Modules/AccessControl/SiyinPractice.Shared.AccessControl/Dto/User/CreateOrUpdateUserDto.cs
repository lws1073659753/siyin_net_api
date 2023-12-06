using SiyinPractice.Shared.Core.Dto;
using System;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    public abstract class CreateOrUpdateUserDto : IInputDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        ///// <summary>
        ///// 头像
        ///// </summary>
        ////public string Avatar { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public Guid? DeptId { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        ///// <summary>
        ///// 角色Id列表，以逗号分隔
        ///// </summary>
        ////public string RoleId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        public int Status { get; set; }

        ///// <summary>
        ///// 账户版本号
        ///// </summary>
        //public int? Version { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 钉钉号
        /// </summary>
        public string DingDing { get; set; }
    }
}