using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithName(GlobalPropertyDisplayName.UpdateId);
        }
    }
}
