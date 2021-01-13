using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommandHandler : IRequestHandler<CreateSystemUserCommand, long>
    {
        private readonly IWeatherLrtContext _context;

        public CreateSystemUserCommandHandler(IWeatherLrtContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
        {
            var systemUser = new SystemUser
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.SystemUsers.Add(systemUser);

            await _context.SaveChangesAsync(cancellationToken);

            return systemUser.SystemUserId;
        }
    }
}
