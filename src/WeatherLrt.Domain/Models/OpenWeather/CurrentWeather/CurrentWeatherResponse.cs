using System.Collections.Generic;

namespace WeatherLrt.Domain.Models.OpenWeather.CurrentWeather
{
    public sealed partial class CurrentWeatherResponse
    {
        private const string SuccessCode = "200";

        public CurrentWeatherResponse()
        {
            Weather = new List<CurrentWeatherResponseWeather>();
            Main = new CurrentWeatherResponseMain();
            Wind = new CurrentWeatherResponseWind();
            Clouds = new CurrentWeatherResponseClouds();
        }

        public CurrentWeatherResponse(string cod, string message)
        {
            Cod = cod;
            Message = message;
        }

        public string Cod { get; set; }

        public string Message { get; set; }

        public List<CurrentWeatherResponseWeather> Weather { get; set; }

        public CurrentWeatherResponseMain Main { get; set; }

        public CurrentWeatherResponseWind Wind { get; set; }

        public CurrentWeatherResponseClouds Clouds { get; set; }

        public bool IsSuccess => Cod == SuccessCode;
    }
}
