using System.Threading.Tasks;
using AutoMapper;
using WeatherLrt.Application.Exceptions;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Application.Queries.ClothingItems.Get;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Domain.Enumerations;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Queries.ClothingItems
{
    public sealed class GetClothingItemQueryTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly GetClothingItemQueryHandler _handler;

        public GetClothingItemQueryTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();
            _handler = new GetClothingItemQueryHandler(_context, new MapperConfiguration(cfg => cfg.AddProfile(new ClothingItemProfile())).CreateMapper());
        }

        [Fact]
        public async Task Handle_GivenAnExistentClothingItemId_ShouldReturnOneClothingItem()
        {
            var clothingItem = new ClothingItem("Skinny jeans");
            clothingItem.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Hot));

            _context.ClothingItems.Add(clothingItem);

            await _context.SaveChangesAsync(default);

            var request = new GetClothingItemQuery(clothingItem.ClothingItemId);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Equal(clothingItem.Description, response.Description);
            Assert.Equal(clothingItem.CreatedOn, response.CreatedOn);
            Assert.Equal(clothingItem.ModifiedOn, response.ModifiedOn);
            Assert.Single(response.Weathers);
        }

        [Fact]
        public async Task Handle_GivenAnInexistentClothingItemId_ShouldRaiseAnException()
        {
            var request = new GetClothingItemQuery(1);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, default));
        }
    }
}
