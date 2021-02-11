using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.Application.Profiles;
using WeatherLrt.Application.Queries.ClothingItems.Search;
using WeatherLrt.Application.Queries.SystemUsers.Search;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Domain.Enumerations;
using WeatlherLrt.Application.Test.Persistence;
using Xunit;

namespace WeatlherLrt.Application.Test.Queries.ClothingItems
{
    public sealed class SearchClothingItemQueryTest
    {
        private readonly IWeatherLrtContext _context;
        private readonly SearchClothingItemQueryHandler _handler;

        public SearchClothingItemQueryTest()
        {
            _context = WeatherLrtTestContextFactory.CreateContextForSQLite();
            _handler = new SearchClothingItemQueryHandler(_context, new MapperConfiguration(cfg => cfg.AddProfile(new ClothingItemProfile())).CreateMapper());
        }

        [Fact]
        public async Task Handle_GivenAnExistentDescriptionAndWeatherType_ShouldReturnOneClothingItem()
        {
            var clothingItemA = new ClothingItem("Skinny jeans");
            clothingItemA.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Hot));

            var clothingItemB = new ClothingItem("Dress");
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Cold));
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Soft));

            _context.ClothingItems.Add(clothingItemA);
            _context.ClothingItems.Add(clothingItemB);

            await _context.SaveChangesAsync(default);

            var request = new SearchClothingItemQuery("Skinny jeans", WeatherType.Hot);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Single(response);
            Assert.Equal(clothingItemA.Description, response.Single().Description);
            Assert.Equal(clothingItemA.CreatedOn, response.Single().CreatedOn);
            Assert.Equal(clothingItemA.ModifiedOn, response.Single().ModifiedOn);
            Assert.Single(response.Single().Weathers);
        }

        [Fact]
        public async Task Handle_GivenAnExistentDescription_ShouldReturnOneClothingItem()
        {
            var clothingItemA = new ClothingItem("Skinny jeans");
            clothingItemA.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Hot));

            var clothingItemB = new ClothingItem("Dress");
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Cold));
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Soft));

            _context.ClothingItems.Add(clothingItemA);
            _context.ClothingItems.Add(clothingItemB);

            await _context.SaveChangesAsync(default);

            var request = new SearchClothingItemQuery("Dress", default);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Single(response);
            Assert.Equal(clothingItemB.Description, response.Single().Description);
            Assert.Equal(clothingItemB.CreatedOn, response.Single().CreatedOn);
            Assert.Equal(clothingItemB.ModifiedOn, response.Single().ModifiedOn);
            Assert.Equal(2, response.Single().Weathers.Count);
        }

        [Fact]
        public async Task Handle_GivenAnExistentWeather_ShouldReturnTwoClothingItems()
        {
            var clothingItemA = new ClothingItem("Skinny jeans");
            clothingItemA.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Hot));

            var clothingItemB = new ClothingItem("Dress");
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Cold));
            clothingItemB.ClothingItemWeathers.Add(new ClothingItemWeather(WeatherType.Hot));

            _context.ClothingItems.Add(clothingItemA);
            _context.ClothingItems.Add(clothingItemB);

            await _context.SaveChangesAsync(default);

            var request = new SearchClothingItemQuery(default, WeatherType.Hot);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task Handle_GivenAnInexistentNameAndEmail_ShouldNotReturnAClothingItem()
        {
            var request = new SearchClothingItemQuery("name A", WeatherType.Cold);

            var response = await _handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
