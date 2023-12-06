using SiyinPractice.Shared.AccessControl.Dto;
using FluentValidation;

namespace SiyinPractice.Shared.AccessControl.DtoValidators;

/// <summary>
/// DeptUpdationDtoValidator
/// </summary>
public class DeptUpdationDtoValidator : AbstractValidator<UpdateDepartmentDto>
{
    /// <summary>
    /// DeptUpdationDtoValidator
    /// </summary>
    public DeptUpdationDtoValidator()
    {
        Include(new DeptCreationDtoValidator());
    }
}