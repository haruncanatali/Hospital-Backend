using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithName(GlobalPropertyDisplayName.Name);
    }
}