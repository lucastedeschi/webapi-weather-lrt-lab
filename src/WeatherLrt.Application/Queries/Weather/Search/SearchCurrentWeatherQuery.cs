using MediatR;

namespace WeatherLrt.Application.Queries.Weather.Search
{
    public sealed class SearchCurrentWeatherQuery : IRequest<SearchCurrentWeatherQueryResponse>
    {
        public SearchCurrentWeatherQuery(string cityName)
        {
            CityName = cityName;
        }

        public string CityName { get; }
    }
}
