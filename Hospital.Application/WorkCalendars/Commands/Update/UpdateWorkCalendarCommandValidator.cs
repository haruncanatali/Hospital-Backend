using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.WorkCalendars.Commands.Update;

public class UpdateWorkCalendarCommandValidator : AbstractValidator<UpdateWorkCalendarCommand>
{
    public UpdateWorkCalendarCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Date).NotNull()
            .WithName(GlobalPropertyDisplayName.WorkCalendarDate);
        RuleFor(c => c.CategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.CategoryId);
        RuleFor(c => c.StaffId).NotNull()
            .WithName(GlobalPropertyDisplayName.StaffId);
    }
}