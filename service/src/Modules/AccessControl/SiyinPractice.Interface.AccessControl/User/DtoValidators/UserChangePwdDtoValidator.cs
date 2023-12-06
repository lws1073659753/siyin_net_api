using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators
{
    public class UserChangePwdDtoValidator : AbstractValidator<ChangeUserPwdDto>
    {
        public UserChangePwdDtoValidator()
        {
            RuleFor(x => x.Password).NotEmpty().Length(5, UserConsts.Password_Maxlength);
            RuleFor(x => x.RePassword).NotEmpty().Length(5, UserConsts.Password_Maxlength)
                                      .Must((dto, rePassword) =>
                                      {
                                          return dto.Password == rePassword;
                                      })
                                      .WithMessage("重复密码必须跟新密码一样");
        }
    }
}