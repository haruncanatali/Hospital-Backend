using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Appointments.Commands.CreateWithQueue;

public class CreateAppointmentWithQueueCommandValidator : AbstractValidator<CreateAppointmentWithQueueCommand>
{
    public CreateAppointmentWithQueueCommandValidator()
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