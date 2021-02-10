namespace WeatherLrt.Infra.CrossCutting.Configuration.Settings
{
    public sealed class OpenWeatherSettings
    {
        public string Endpoint { get; set; }

        public string AppId { get; set; }

        public string UnitsDefault { get; set; }

        public string LanguageDefault { get; set; }
    }
}
