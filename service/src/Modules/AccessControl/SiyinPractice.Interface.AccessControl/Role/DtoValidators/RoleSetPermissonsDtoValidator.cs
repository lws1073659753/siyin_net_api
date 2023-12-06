using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

public class RoleSetPermissonsDtoValidator : AbstractValidator<RoleSetPermissonsDto>
{
    public RoleSetPermissonsDtoValidator()
    {
        //RuleFor(x => x.RoleId).GreaterThan(0);
        RuleFor(x => x.Permissions).NotNull();
    }
}