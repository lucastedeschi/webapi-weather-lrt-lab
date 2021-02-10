using MediatR;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignIn
{
    public sealed class SignInApplicationUserCommand : IRequest<SignInApplicationUserCommandResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
