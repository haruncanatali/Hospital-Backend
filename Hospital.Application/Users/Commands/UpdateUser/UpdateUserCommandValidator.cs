using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithName(GlobalPropertyDisplayName.UpdateId);
        }
    }
}
