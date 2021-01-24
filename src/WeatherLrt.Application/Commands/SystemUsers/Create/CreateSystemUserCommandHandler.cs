using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommandHandler : IRequestHandler<CreateSystemUserCommand, CreateSystemUserCommandResponse>
    {
        private readonly IWeatherLrtContext _context;

        public CreateSystemUserCommandHandler(IWeatherLrtContext context)
        {
            _context = context;
        }

        public async Task<CreateSystemUserCommandResponse> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateSystemUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new CreateSystemUserCommandResponse(validationResult.Errors);

            var systemUser = new SystemUser
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.SystemUsers.Add(systemUser);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateSystemUserCommandResponse(systemUser.SystemUserId);
        }
    }
}
