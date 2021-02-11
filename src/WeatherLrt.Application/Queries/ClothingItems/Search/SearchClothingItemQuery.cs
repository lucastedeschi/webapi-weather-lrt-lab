using System.Collections.Generic;
using MediatR;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Application.Queries.ClothingItems.Search
{
    public sealed class SearchClothingItemQuery : IRequest<List<ClothingItemResponse>>
    {
        public SearchClothingItemQuery(string description = null, WeatherType? weatherType = null)
        {
            Description = description;
            WeatherType = weatherType;
        }

        public string Description { get; }

        public WeatherType? WeatherType { get; }
    }
}
