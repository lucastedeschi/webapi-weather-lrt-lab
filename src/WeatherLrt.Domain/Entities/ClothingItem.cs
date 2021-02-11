using System.Collections.Generic;

namespace WeatherLrt.Domain.Entities
{
    public class ClothingItem : Auditable
    {
        public ClothingItem()
        {
            ClothingItemWeathers = new HashSet<ClothingItemWeather>();
        }

        public ClothingItem(string description) : this()
        {
            Description = description;
        }

        public long ClothingItemId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ClothingItemWeather> ClothingItemWeathers { get; private set; }
    }
}
