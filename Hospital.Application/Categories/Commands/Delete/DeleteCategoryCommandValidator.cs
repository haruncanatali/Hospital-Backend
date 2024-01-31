using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithName(GlobalPropertyDisplayName.UpdateId);
    }
}