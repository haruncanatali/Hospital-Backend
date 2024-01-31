using FluentValidation;
using Hospital.Application.Common.Models;

namespace Hospital.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithName(GlobalPropertyDisplayName.FirstName);
            RuleFor(x => x.LastName).NotEmpty().WithName(GlobalPropertyDisplayName.LastName);
            RuleFor(x => x.IdentityNumber).Length(11).NotEmpty().WithName(GlobalPropertyDisplayName.IdentityNumber);
            RuleFor(x => x.Email).EmailAddress().WithName(GlobalPropertyDisplayName.Email);
            RuleFor(x => x.Gender).NotNull().WithMessage("'Cinsiyet' alanı boş olmamalı.");
            RuleFor(x => x.RoleName).NotEmpty().WithName(GlobalPropertyDisplayName.RoleName);
        }
    }
}