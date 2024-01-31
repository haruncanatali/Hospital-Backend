using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Patients.Commands.Update;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.UpdateId);
        RuleFor(c => c.Address).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Address);
        RuleFor(c => c.Nationality).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Nationality);
    }
}