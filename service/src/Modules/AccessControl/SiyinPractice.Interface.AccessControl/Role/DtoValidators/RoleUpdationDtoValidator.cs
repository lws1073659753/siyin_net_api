using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

public class RoleUpdationDtoValidator : AbstractValidator<UpdateRoleDto>
{
    public RoleUpdationDtoValidator()
    {
        Include(new RoleCreationDtoValidator());
    }
}