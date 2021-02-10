using MediatR;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignUp
{
    public sealed class SignUpApplicationUserCommand : IRequest<SignUpApplicationUserCommandResponse>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
