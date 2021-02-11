using System;
using System.Collections.Generic;

namespace WeatherLrt.Application.Queries.Common
{
    public sealed class ClothingItemResponse
    {
        public ClothingItemResponse()
        {
            Weathers = new List<string>();
        }

        public long ClothingItemId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<string> Weathers { get; set; }
    }
}
