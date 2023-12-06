using SiyinPractice.Shared.Core.Dto;

namespace SiyinPractice.Shared.AccessControl.Dto
{
    /// <summary>
    /// 刷新Token实体
    /// </summary>
    public class UserRefreshTokenDto : IInputDto
    {
        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}