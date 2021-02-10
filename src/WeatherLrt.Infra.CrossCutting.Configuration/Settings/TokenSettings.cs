namespace WeatherLrt.Infra.CrossCutting.Configuration.Settings
{
    public sealed class TokenSettings
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int Seconds { get; set; }
    }
}
