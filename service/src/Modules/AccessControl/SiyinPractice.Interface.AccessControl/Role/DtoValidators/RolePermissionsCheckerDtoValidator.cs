using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;
using System.Linq;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

public class RolePermissionsCheckerDtoValidator : AbstractValidator<RolePermissionsCheckerDto>
{
    public RolePermissionsCheckerDtoValidator()
    {
        RuleFor(x => x.RoleIds).NotNull().Must(x => x.Count() > 0);
        RuleFor(x => x.Permissions).NotNull().Must(x => x.Count() > 0);
    }
}