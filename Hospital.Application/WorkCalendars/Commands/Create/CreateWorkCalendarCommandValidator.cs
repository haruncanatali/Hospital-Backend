using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.WorkCalendars.Commands.Create;

public class CreateWorkCalendarCommandValidator : AbstractValidator<CreateWorkCalendarCommand>
{
    public CreateWorkCalendarCommandValidator()
    {
        RuleFor(c => c.Date).NotNull()
            .WithName(GlobalPropertyDisplayName.WorkCalendarDate);
        RuleFor(c => c.CategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.CategoryId);
        RuleFor(c => c.StaffId).NotNull()
            .WithName(GlobalPropertyDisplayName.StaffId);
    }
}