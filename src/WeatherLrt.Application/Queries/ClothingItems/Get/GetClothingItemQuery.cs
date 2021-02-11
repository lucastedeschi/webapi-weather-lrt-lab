using MediatR;
using WeatherLrt.Application.Queries.Common;

namespace WeatherLrt.Application.Queries.ClothingItems.Get
{
    public sealed class GetClothingItemQuery : IRequest<ClothingItemResponse>
    {
        public GetClothingItemQuery(long clothingItemId)
        {
            ClothingItemId = clothingItemId;
        }

        public long ClothingItemId { get; }
    }
}
