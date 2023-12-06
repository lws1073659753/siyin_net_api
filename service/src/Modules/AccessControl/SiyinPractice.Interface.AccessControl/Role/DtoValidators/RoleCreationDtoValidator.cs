using SiyinPractice.Domain.Shared.AccessControl;
using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

public class RoleCreationDtoValidator : AbstractValidator<CreateRoleDto>
{
    public RoleCreationDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, RoleConsts.Name_MaxLength);
        RuleFor(x => x.Tips).NotEmpty().Length(2, RoleConsts.Tips_MaxLength);
    }
}