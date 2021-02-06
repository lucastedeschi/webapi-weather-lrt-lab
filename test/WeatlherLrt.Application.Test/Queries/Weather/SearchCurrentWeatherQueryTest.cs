using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Application.Queries.Weather.Search;
using WeatherLrt.Domain.Models.OpenWeather.CurrentWeather;
using Xunit;

namespace WeatlherLrt.Application.Test.Queries.Weather
{
    public sealed class SearchCurrentWeatherQueryTest
    {
        private readonly Mock<IOpenWeatherServiceClient> _openWeatherServiceClient;
        private readonly SearchCurrentWeatherQueryHandler _handler;

        public SearchCurrentWeatherQueryTest()
        {
            _openWeatherServiceClient = new Mock<IOpenWeatherServiceClient>();
            _handler = new SearchCurrentWeatherQueryHandler(_openWeatherServiceClient.Object, new MapperConfiguration(cfg => cfg.AddProfile(new WeatherProfile())).CreateMapper());
        }

        [Fact]
        public async Task Handle_GivenAValidCityNameWithASuccessResponse_ShouldReturnTheCityCurrentWeather()
        {
            var currentWeatherResponse = new CurrentWeatherResponse
            {
                Cod = "200",
                Main = new CurrentWeatherResponseMain
                {
                    Temp = 29.8M,
                    TempMax = 32.0M,
                    TempMin = 27.9M,
                    FeelsLike = 31.2M,
                    Humidity = 83,
                    Pressure = 1012
                },
                Clouds = new CurrentWeatherResponseClouds
                {
                    All = 90
                },
                Wind = new CurrentWeatherResponseWind
                {
                    Deg = 160,
                    Speed = 4.63M
                },
                Weather = new System.Collections.Generic.List<CurrentWeatherResponseWeather>
                {
                    new CurrentWeatherResponseWeather
                    {
                        Id = 804,
                        Description = "nublado"
                    }
                }
            };

            _openWeatherServiceClient.Setup(s => s.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(() => currentWeatherResponse);

            var request = new SearchCurrentWeatherQuery("Piracicaba");

            var response = await _handler.Handle(request, default);

            Assert.Equal(29.8M, response.Temperature);
            Assert.Equal(32.0M, response.MaximumTemperature);
            Assert.Equal(27.9M, response.MinimumTemperature);
            Assert.Equal(31.2M, response.FeelsLike);
            Assert.Equal(83, response.Humidity);
            Assert.Equal(1012, response.Pressure);
            Assert.Equal(90, response.Clouds);
            Assert.Equal(160, response.WindDegree);
            Assert.Equal(4.63M, response.WindSpeed);
        }

        [Fact]
        public async Task Handle_GivenAnInvalidCityNameWithAFailureResponse_ShouldRaiseAnException()
        {
            var currentWeatherResponse = new CurrentWeatherResponse
            {
                Cod = "404",
                Message = "city not found"
            };

            _openWeatherServiceClient.Setup(s => s.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(() => currentWeatherResponse);

            var request = new SearchCurrentWeatherQuery("Piracicab");

            await Assert.ThrowsAsync<InvalidRequestException>(async () => await _handler.Handle(request, default));
        }
    }
}
