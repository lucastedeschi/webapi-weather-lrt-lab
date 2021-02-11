using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Queries.ClothingItems.Search;
using WeatherLrt.Application.Queries.Common;

namespace WeatherLrt.Application.Queries.SystemUsers.Search
{
    public sealed class SearchClothingItemQueryHandler : IRequestHandler<SearchClothingItemQuery, List<ClothingItemResponse>>
    {
        private readonly IWeatherLrtContext _context;
        private readonly IMapper _mapper;

        public SearchClothingItemQueryHandler(IWeatherLrtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClothingItemResponse>> Handle(SearchClothingItemQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ClothingItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Description))
                query = query.Where(c => c.Description == request.Description);

            if (request.WeatherType.HasValue)
                query = query.Where(c => c.ClothingItemWeathers.Any(i => i.WeatherType == request.WeatherType.Value));

            var clothingItems = await query.ToListAsync();

            return _mapper.Map<List<ClothingItemResponse>>(clothingItems);
        }
    }
}
