using System;

namespace WeatherLrt.Domain.Entities
{
    public abstract class Auditable
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
