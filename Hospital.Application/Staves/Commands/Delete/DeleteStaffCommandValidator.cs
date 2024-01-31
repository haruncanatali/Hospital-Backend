using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Staves.Commands.Delete;

public class DeleteStaffCommandValidator : AbstractValidator<DeleteStaffCommand>
{
    public DeleteStaffCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}