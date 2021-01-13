namespace WeatherLrt.Domain.Entities
{
    public sealed class SystemUser : Auditable
    {
        public long SystemUserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
