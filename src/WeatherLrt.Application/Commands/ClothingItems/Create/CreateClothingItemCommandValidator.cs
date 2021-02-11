using FluentValidation;
using WeatherLrt.Application.Extensions;
using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Application.Commands.ClothingItems.Create
{
    public sealed class CreateClothingItemCommandValidator : AbstractValidator<CreateClothingItemCommand>
    {
        public CreateClothingItemCommandValidator()
        {
            RuleFor(e => e.Description).NotEmpty();

            RuleFor(e => e.Weathers)
                .NotEmpty()
                .ForEach(weather => weather.SubsetOf(typeof(WeatherType)));
        }
    }
}
