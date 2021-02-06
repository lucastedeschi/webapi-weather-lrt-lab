namespace WeatherLrt.Infra.CrossCutting.Configuration.Options
{
    public sealed class OpenWeatherOptions
    {
        public string Endpoint { get; set; }

        public string AppId { get; set; }

        public string UnitsDefault { get; set; }

        public string LanguageDefault { get; set; }
    }
}
