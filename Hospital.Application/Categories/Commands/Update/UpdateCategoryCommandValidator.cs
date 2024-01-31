using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Name).NotEmpty().WithName(GlobalPropertyDisplayName.Name);
        RuleFor(c => c.EntityStatus).NotNull().WithName(GlobalPropertyDisplayName.EntityStatus);
    }
}