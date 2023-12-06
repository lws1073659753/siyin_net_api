using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators
{
    public class UserUpdationDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UserUpdationDtoValidator()
        {
            Include(new UserCreationAndUpdationDtoValidator());
        }
    }
}