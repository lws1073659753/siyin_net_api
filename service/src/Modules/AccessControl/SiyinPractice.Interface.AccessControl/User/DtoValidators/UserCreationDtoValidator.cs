using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators
{
    public class UserCreationDtoValidator : AbstractValidator<CreateUserDto>
    {
        public UserCreationDtoValidator()
        {
            Include(new UserCreationAndUpdationDtoValidator());
            RuleFor(x => x.Password).NotEmpty().Length(5, UserConsts.Password_Maxlength);
            //RuleFor(x => x.Password).NotEmpty().When(x => x.Id < 1)
            //                        .Length(5, 16).When(x => x.Id < 1);
        }
    }
}