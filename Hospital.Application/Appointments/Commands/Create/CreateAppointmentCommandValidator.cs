using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Appointments.Commands.Create;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotNull()
            .WithName(GlobalPropertyDisplayName.CategoryId);
        RuleFor(c => c.PatientId).NotNull()
            .WithName(GlobalPropertyDisplayName.PatientId);
        RuleFor(c => c.StaffId).NotNull()
            .WithName(GlobalPropertyDisplayName.StaffId);
        RuleFor(c => c.WorkCalendarId).NotNull()
            .WithName(GlobalPropertyDisplayName.WorkCalendarId);
    }
}