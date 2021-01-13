using MediatR;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommand : IRequest<long>
    {
        public string Name { get; }

        public string Email { get; }
    }
}
