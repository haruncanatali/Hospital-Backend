using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Roles.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.RoleName);
    }
}