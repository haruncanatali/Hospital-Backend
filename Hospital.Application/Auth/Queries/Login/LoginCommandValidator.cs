using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithName(GlobalPropertyDisplayName.Password);
            RuleFor(x => x.UserName).NotEmpty().WithName(GlobalPropertyDisplayName.UserName);
        }
    }
}
