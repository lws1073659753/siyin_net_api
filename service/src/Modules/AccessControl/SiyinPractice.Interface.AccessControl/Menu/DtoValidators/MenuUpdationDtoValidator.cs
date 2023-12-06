using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

public class MenuUpdationDtoValidator : AbstractValidator<UpdateMenuDto>
{
    public MenuUpdationDtoValidator()
    {
        Include(new MenuCreationDtoValidator());
    }
}