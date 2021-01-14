using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Queries.SystemUsers.Get
{
    public sealed class GetSystemUserQueryHandler : IRequestHandler<GetSystemUserQuery, SystemUserResponse>
    {
        private readonly IWeatherLrtContext _context;
        private readonly IMapper _mapper;

        public GetSystemUserQueryHandler(IWeatherLrtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SystemUserResponse> Handle(GetSystemUserQuery request, CancellationToken cancellationToken)
        {
            var systemUser = await _context.SystemUsers.FindAsync(request.SystemUserId);

            if (systemUser == null)
                throw new NotFoundException(nameof(SystemUser), request.SystemUserId);

            return _mapper.Map<SystemUserResponse>(systemUser);
        }
    }
}
