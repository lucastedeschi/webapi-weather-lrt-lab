using System;

namespace WeatherLrt.Application.Queries.Common
{
    public sealed class SystemUserResponse
    {
        public long SystemUserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
