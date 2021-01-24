using MediatR;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommand : IRequest<CreateSystemUserCommandResponse>
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
