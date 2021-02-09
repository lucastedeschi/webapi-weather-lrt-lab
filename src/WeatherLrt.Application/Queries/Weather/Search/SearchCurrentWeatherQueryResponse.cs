namespace WeatherLrt.Application.Queries.Weather.Search
{
    public sealed class SearchCurrentWeatherQueryResponse
    {
        public decimal Temperature { get; set; }

        public decimal FeelsLike { get; set; }

        public decimal MaximumTemperature { get; set; }

        public decimal MinimumTemperature { get; set; }

        public decimal Pressure { get; set; }

        public int Humidity { get; set; }

        public decimal WindSpeed { get; set; }

        public int WindDegree { get; set; }

        public int Clouds { get; set; }
    }
}
