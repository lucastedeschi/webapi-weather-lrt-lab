using System.Collections.Generic;
using FluentValidation.Results;
using WeatherLrt.Application.Commands.Common;

namespace WeatherLrt.Application.Commands.ClothingItems.Create
{
    public sealed class CreateClothingItemCommandResponse : CommandResponseBase
    {
        public CreateClothingItemCommandResponse(long clothingItemId)
        {
            ClothingItemId = clothingItemId;
        }

        public CreateClothingItemCommandResponse(IEnumerable<ValidationFailure> validations) : base(validations)
        {
        }

        public long ClothingItemId { get; }
    }
}
