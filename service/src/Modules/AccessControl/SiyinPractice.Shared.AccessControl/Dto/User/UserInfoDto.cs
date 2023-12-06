using SiyinPractice.Shared.Core.Dto;
using System;
using System.Collections.Generic;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoDto : IDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 基本信息
        /// </summary>
        public UserProfileDto Profile { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public List<string> Roles { get; private set; } = new List<string>();

        /// <summary>
        /// 权限集合
        /// </summary>
        public List<string> Permissions { get; private set; } = new List<string>();
    }
}