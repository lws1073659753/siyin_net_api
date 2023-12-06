using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    /// <summary>
    /// 修改密码数据模型
    /// </summary>
    public class ChangeUserPwdDto : IInputDto
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string RePassword { get; set; }
    }
}