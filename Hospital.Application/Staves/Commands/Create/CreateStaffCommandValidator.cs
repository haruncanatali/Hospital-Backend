using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Staves.Commands.Create;

public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    public CreateStaffCommandValidator()
    {
        RuleFor(c => c.Resume).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Resume);
        RuleFor(c => c.Title).NotNull()
            .WithName(GlobalPropertyDisplayName.Title);
        RuleFor(c => c.UserId).NotNull()
            .WithName(GlobalPropertyDisplayName.UserId);
        RuleFor(c => c.CategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.CategoryId);
    }
}