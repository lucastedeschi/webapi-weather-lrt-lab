using FluentValidation;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommandValidator : AbstractValidator<CreateSystemUserCommand>
    {
        public CreateSystemUserCommandValidator()
        {
            RuleFor(e => e.Email).MinimumLength(3);

            RuleFor(e => e.Name).NotEmpty();
        }
    }
}
