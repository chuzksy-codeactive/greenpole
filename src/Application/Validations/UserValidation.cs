using Application.DTOs;
using FluentValidation;

namespace Application.Validations
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be null or empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Enter a Valid Email Address")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName cannot be null or empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be null or empty");

        }
    }
}
