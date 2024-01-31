using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Patients.Commands.Delete;

public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
{
    public DeletePatientCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
    }
}