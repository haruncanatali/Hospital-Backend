using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Appointments.Commands.Delete;

public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}