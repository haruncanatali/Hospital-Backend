using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.WorkCalendars.Commands.Delete;

public class DeleteWorkCalendarCommandValidator : AbstractValidator<DeleteWorkCalendarCommand>
{
    public DeleteWorkCalendarCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}