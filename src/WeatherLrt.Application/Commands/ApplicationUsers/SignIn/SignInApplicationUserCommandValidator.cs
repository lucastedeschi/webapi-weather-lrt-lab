using FluentValidation;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignIn
{
    public sealed class SignInApplicationUserCommandValidator : AbstractValidator<SignInApplicationUserCommand>
    {
        public SignInApplicationUserCommandValidator()
        {
            RuleFor(e => e.Email).MinimumLength(3);

            RuleFor(e => e.Password).NotEmpty();
        }
    }
}
