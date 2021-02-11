using System.Collections.Generic;
using MediatR;

namespace WeatherLrt.Application.Commands.ClothingItems.Create
{
    public sealed class CreateClothingItemCommand : IRequest<CreateClothingItemCommandResponse>
    {
        public string Description { get; set; }

        public List<string> Weathers { get; set; }
    }
}
