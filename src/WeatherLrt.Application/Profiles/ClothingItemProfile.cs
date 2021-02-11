using System;
using System.Linq;
using AutoMapper;
using WeatherLrt.Application.Queries.Common;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Application.Profiles
{
    public sealed class ClothingItemProfile : Profile
    {
        public ClothingItemProfile()
        {
            CreateMap<ClothingItem, ClothingItemResponse>()
                .ForMember(d => d.Weathers, o => o.MapFrom(p => p.ClothingItemWeathers.Select(c => Enum.GetName(typeof(WeatherType), c.WeatherType))));
        }
    }
}
