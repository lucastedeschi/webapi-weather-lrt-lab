using Newtonsoft.Json;

namespace WeatherLrt.Domain.Models.OpenWeather.CurrentWeather
{
    public sealed class CurrentWeatherResponseMain
    {
        public decimal Temp { get; set; }

        [JsonProperty(PropertyName = "feels_like")]
        public decimal FeelsLike { get; set; }

        [JsonProperty(PropertyName = "temp_min")]
        public decimal TempMin { get; set; }

        [JsonProperty(PropertyName = "temp_max")]
        public decimal TempMax { get; set; }

        public decimal Pressure { get; set; }

        public decimal Humidity { get; set; }
    }
}
