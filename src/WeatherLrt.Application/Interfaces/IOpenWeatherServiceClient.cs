using System.Threading.Tasks;
using WeatherLrt.Domain.Models.OpenWeather.CurrentWeather;

namespace WeatherLrt.Application.Interfaces
{
    public interface IOpenWeatherServiceClient
    {
        Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string cityName);
    }
}
