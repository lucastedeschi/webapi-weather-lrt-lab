using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Models.OpenWeather.CurrentWeather;

namespace WeatherLrt.Services.OpenWeather
{
    public sealed class OpenWeatherServiceClient : IOpenWeatherServiceClient
    {
        private const string OpenWeatherCurrentClientName = "OpenWeatherCurrentClientName";
        private const string Endpoint = "http://api.openweathermap.org/data/2.5";
        private const string AppId = "0cc25201bf9bd27c53917b8ead2508e0";
        private const string InternalErrorMessage = "An error has occurred while communicating with OpenWeatherMap. Please contact the support team";
        private const string InternalErrorStatus = "500";

        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherServiceClient> _logger;

        public OpenWeatherServiceClient(IHttpClientFactory clientFactory, ILogger<OpenWeatherServiceClient> logger)
        {
            _httpClient = clientFactory.CreateClient(OpenWeatherCurrentClientName);
            _logger = logger;
        }

        public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string cityName)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{Endpoint}/weather?q={cityName}&units=metric&land=en&appid={AppId}")
                };

                using var httpClient = _httpClient;

                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                return JsonConvert.DeserializeObject<CurrentWeatherResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            } catch(Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return new CurrentWeatherResponse(InternalErrorStatus, InternalErrorMessage);
            }
        }
    }
}
