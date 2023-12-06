namespace SiyinPractice.Shared.AccessControl.Dto
{
    public class CreateUserDto : CreateOrUpdateUserDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}