using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Domain.Entities
{
    public class ClothingItemWeather
    {
        public ClothingItemWeather()
        {
        }

        public ClothingItemWeather(WeatherType weatherType)
        {
            WeatherType = weatherType;
        }

        public long ClothingItemWeatherId { get; set; }

        public long ClothingItemId { get; set; }

        public WeatherType WeatherType { get; set; }

        public ClothingItem ClothingItem { get; private set; }
    }
}
