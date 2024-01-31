using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Roles.Commands.AddToRole;

public class AddToRoleCommandValidator : AbstractValidator<AddToRoleCommand>
{
    public AddToRoleCommandValidator()
    {
        RuleFor(c => c.RoleId).NotNull()
            .WithName(GlobalPropertyDisplayName.RoleId);
        RuleFor(c => c.UserId).NotNull()
            .WithName(GlobalPropertyDisplayName.UserId);
    }
}