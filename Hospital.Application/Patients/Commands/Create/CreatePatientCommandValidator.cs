using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Patients.Commands.Create;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(c => c.Nationality).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Nationality);
        RuleFor(c => c.Address).NotEmpty()
            .WithName(GlobalPropertyDisplayName.Address);
        RuleFor(c => c.UserId).NotNull()
            .WithName(GlobalPropertyDisplayName.UserId);
    }
}