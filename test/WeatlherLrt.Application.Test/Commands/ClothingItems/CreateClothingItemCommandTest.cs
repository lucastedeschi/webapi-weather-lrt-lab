using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherLrt.Application.Commands.ClothingItems.Create;
using WeatherLrt.Application.Interfaces;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Commands.ClothingItems
{
    public sealed class CreateClothingItemCommandTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly CreateClothingItemCommandHandler _handler;

        public CreateClothingItemCommandTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();

            _handler = new CreateClothingItemCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_GivenAValidRequest_ShouldCreateOneClothingItem()
        {
            var request = new CreateClothingItemCommand
            {
                Description = "Dress",
                Weathers = new List<string>
                {
                    "Cold",
                    "Hot"
                }
            };

            var response = await _handler.Handle(request, default);

            var clothingItem = await _context.ClothingItems.FindAsync(response.ClothingItemId);

            Assert.Empty(response.Errors);
            Assert.NotNull(clothingItem);
            Assert.Equal(request.Description, clothingItem.Description);
            Assert.NotEqual(default, clothingItem.CreatedOn);
            Assert.NotEqual(default, clothingItem.ModifiedOn);
            Assert.Equal(clothingItem.CreatedOn, clothingItem.ModifiedOn);
            Assert.Equal(2, clothingItem.ClothingItemWeathers.Count);
        }

        [Fact]
        public async Task Handler_GivenARequestWithInvalidWeatherAndEmptyDescription_ShouldRejectWithTwoValidations()
        {
            var request = new CreateClothingItemCommand
            {
                Description = string.Empty,
                Weathers = new List<string>
                {
                    "Sun"
                }
            };

            var response = await _handler.Handle(request, default);

            Assert.NotEmpty(response.Errors);
            Assert.Equal(2, response.Errors.Count());
        }

        [Fact]
        public async Task Handler_GivenARequestWithEmptyDescriptionAndWeathers_ShouldRejectWithTwoValidations()
        {
            var request = new CreateClothingItemCommand
            {
                Description = string.Empty,
                Weathers = new List<string>()
            };

            var response = await _handler.Handle(request, default);

            Assert.NotEmpty(response.Errors);
            Assert.Equal(2, response.Errors.Count());
        }
    }
}
