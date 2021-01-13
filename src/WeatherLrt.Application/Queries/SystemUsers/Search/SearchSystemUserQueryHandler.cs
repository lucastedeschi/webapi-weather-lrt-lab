using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Queries.Common;

namespace WeatherLrt.Application.Queries.SystemUsers.Search
{
    public sealed class SearchSystemUserQueryHandler : IRequestHandler<SearchSystemUserQuery, List<SystemUserResponse>>
    {
        private readonly IWeatherLrtContext _context;
        private readonly IMapper _mapper;

        public SearchSystemUserQueryHandler(IWeatherLrtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SystemUserResponse>> Handle(SearchSystemUserQuery request, CancellationToken cancellationToken)
        {
            var query = _context.SystemUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(s => s.Name == request.Name);

            if (!string.IsNullOrWhiteSpace(request.Email))
                query = query.Where(s => s.Email == request.Email);

            return await query.ProjectTo<SystemUserResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
