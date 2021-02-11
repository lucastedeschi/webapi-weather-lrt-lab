using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Commands.ClothingItems.Create;
using WeatherLrt.Application.Queries.ClothingItems.Get;
using WeatherLrt.Application.Queries.ClothingItems.Search;
using WeatherLrt.Domain.Enumerations;
using WeatherLrt.WebApi.Results;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ClothingItemController : WebApiControllerBase<ClothingItemController>
    {
        private readonly IMediator _mediator;

        public ClothingItemController(IMediator mediator, ILogger<ClothingItemController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            return await Handle(
                async () =>
                {
                    if (id == default)
                        return new BadRequestErrorResult("Id must not be empty");

                    var clothingItem = await _mediator.Send(new GetClothingItemQuery(id));

                    return new OkObjectResult(clothingItem);
                });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string description = null, [FromQuery] string weatherType = null)
        {
            return await Handle(
                async () =>
                {
                    if (string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(weatherType))
                        return new BadRequestErrorResult("Description and Weather Type must not be empty");

                    if (!string.IsNullOrWhiteSpace(weatherType) & !Enum.TryParse<WeatherType>(weatherType, out var parsedWeatherType))
                        return new BadRequestErrorResult("Weather Type has a wrong value");

                    var clothingItems = await _mediator.Send(new SearchClothingItemQuery(description, parsedWeatherType));

                    return new OkObjectResult(clothingItems);
                });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClothingItemCommand command)
        {
            return await Handle(
                async () =>
                {
                    if (command is null)
                        return new BadRequestErrorResult("Command has a wrong value");

                    var response = await _mediator.Send(command);

                    if (response.Errors.Any())
                        return new BadRequestErrorResult(response.Errors);

                    var clothingItem = await _mediator.Send(new GetClothingItemQuery(response.ClothingItemId));

                    return new OkObjectResult(clothingItem);
                });
        }
    }
}
