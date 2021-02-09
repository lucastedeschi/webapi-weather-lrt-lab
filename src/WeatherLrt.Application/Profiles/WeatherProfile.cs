using AutoMapper;
using WeatherLrt.Application.Queries.Weather.Search;
using WeatherLrt.Domain.Models.OpenWeather.CurrentWeather;

namespace WeatherLrt.Application.Profiles
{
    public sealed class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<CurrentWeatherResponse, SearchCurrentWeatherQueryResponse>()
                .ForMember(d => d.Temperature, o => o.MapFrom(p => p.Main.Temp))
                .ForMember(d => d.MaximumTemperature, o => o.MapFrom(p => p.Main.TempMax))
                .ForMember(d => d.MinimumTemperature, o => o.MapFrom(p => p.Main.TempMin))
                .ForMember(d => d.FeelsLike, o => o.MapFrom(p => p.Main.FeelsLike))
                .ForMember(d => d.Pressure, o => o.MapFrom(p => p.Main.Pressure))
                .ForMember(d => d.Humidity, o => o.MapFrom(p => p.Main.Humidity))
                .ForMember(d => d.WindDegree, o => o.MapFrom(p => p.Wind.Deg))
                .ForMember(d => d.WindSpeed, o => o.MapFrom(p => p.Wind.Speed))
                .ForMember(d => d.Clouds, o => o.MapFrom(p => p.Clouds.All));
        }
    }
}
