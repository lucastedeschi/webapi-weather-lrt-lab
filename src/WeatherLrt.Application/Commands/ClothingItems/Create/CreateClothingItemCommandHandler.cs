using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Application.Commands.ClothingItems.Create
{
    public sealed class CreateClothingItemCommandHandler : IRequestHandler<CreateClothingItemCommand, CreateClothingItemCommandResponse>
    {
        private readonly IWeatherLrtContext _context;

        public CreateClothingItemCommandHandler(IWeatherLrtContext context)
        {
            _context = context;
        }

        public async Task<CreateClothingItemCommandResponse> Handle(CreateClothingItemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateClothingItemCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return new CreateClothingItemCommandResponse(validationResult.Errors);

            var existingClothingItemId = await _context.ClothingItems
                .Where(c => c.Description == request.Description)
                .Select(c => c.ClothingItemId)
                .FirstOrDefaultAsync();

            if (existingClothingItemId != default)
                return new CreateClothingItemCommandResponse(existingClothingItemId);

            var clothingItem = new ClothingItem(request.Description);

            foreach (var weather in request.Weathers)
                clothingItem.ClothingItemWeathers.Add(new ClothingItemWeather(Enum.Parse<WeatherType>(weather)));

            _context.ClothingItems.Add(clothingItem);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateClothingItemCommandResponse(clothingItem.ClothingItemId);
        }
    }
}
