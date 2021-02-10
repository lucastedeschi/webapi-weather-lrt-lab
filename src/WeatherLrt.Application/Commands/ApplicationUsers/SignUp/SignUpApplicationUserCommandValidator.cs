using FluentValidation;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignUp
{
    public sealed class SignUpApplicationUserCommandValidator : AbstractValidator<SignUpApplicationUserCommand>
    {
        public SignUpApplicationUserCommandValidator()
        {
            RuleFor(e => e.UserName).NotEmpty();

            RuleFor(e => e.Email).MinimumLength(3);

            RuleFor(e => e.Password).NotEmpty();
        }
    }
}
