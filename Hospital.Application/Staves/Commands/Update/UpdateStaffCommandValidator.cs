using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Staves.Commands.Update;

public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
{
    public UpdateStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Resume).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Resume);
        RuleFor(c => c.Title).NotNull()
            .WithName(GlobalPropertyDisplayName.Title);
        RuleFor(c => c.CategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.CategoryId);
    }
}