using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Models.OpenWeather.CurrentWeather;
using WeatherLrt.Infra.CrossCutting.Configuration.Options;

namespace WeatherLrt.Services.OpenWeather
{
    public sealed class OpenWeatherServiceClient : IOpenWeatherServiceClient
    {
        private const string OpenWeatherCurrentClientName = "OpenWeatherCurrentClientName";
        private const string InternalErrorMessage = "An error has occurred while communicating with OpenWeatherMap. Please contact the support team";
        private const string InternalErrorStatus = "500";

        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherServiceClient> _logger;
        private readonly OpenWeatherOptions _openWeatherOptions;

        public OpenWeatherServiceClient(IHttpClientFactory clientFactory, ILogger<OpenWeatherServiceClient> logger, OpenWeatherOptions openWeatherOptions)
        {
            _httpClient = clientFactory.CreateClient(OpenWeatherCurrentClientName);
            _logger = logger;
            _openWeatherOptions = openWeatherOptions;
        }

        public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string cityName)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_openWeatherOptions.Endpoint}/weather?q={cityName}&units={_openWeatherOptions.UnitsDefault}&lang={_openWeatherOptions.LanguageDefault}&appid={_openWeatherOptions.AppId}")
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
