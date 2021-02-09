using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Application.Queries.Weather.Search
{
    public sealed class SearchCurrentWeatherQueryHandler : IRequestHandler<SearchCurrentWeatherQuery, SearchCurrentWeatherQueryResponse>
    {
        private readonly IOpenWeatherServiceClient _openWeatherServiceClient;
        private readonly IMapper _mapper;

        public SearchCurrentWeatherQueryHandler(IOpenWeatherServiceClient openWeatherServiceClient, IMapper mapper)
        {
            _openWeatherServiceClient = openWeatherServiceClient;
            _mapper = mapper;
        }

        public async Task<SearchCurrentWeatherQueryResponse> Handle(SearchCurrentWeatherQuery request, CancellationToken cancellationToken)
        {
            var currentWeather = await _openWeatherServiceClient.GetCurrentWeatherAsync(request.CityName);

            if (!currentWeather.IsSuccess)
                throw new InvalidRequestException(currentWeather.Message);

            return _mapper.Map<SearchCurrentWeatherQueryResponse>(currentWeather);
        }
    }
}
