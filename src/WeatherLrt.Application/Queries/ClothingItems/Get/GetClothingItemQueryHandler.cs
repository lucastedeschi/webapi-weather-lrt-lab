using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Application.Queries.ClothingItems.Get
{
    public sealed class GetClothingItemQueryHandler : IRequestHandler<GetClothingItemQuery, ClothingItemResponse>
    {
        private readonly IWeatherLrtContext _context;
        private readonly IMapper _mapper;

        public GetClothingItemQueryHandler(IWeatherLrtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClothingItemResponse> Handle(GetClothingItemQuery request, CancellationToken cancellationToken)
        {
            var clothingItem = await _context.ClothingItems.FindAsync(request.ClothingItemId);

            if (clothingItem == null)
                throw new NotFoundException(nameof(ClothingItem), request.ClothingItemId);

            return _mapper.Map<ClothingItemResponse>(clothingItem);
        }
    }
}
