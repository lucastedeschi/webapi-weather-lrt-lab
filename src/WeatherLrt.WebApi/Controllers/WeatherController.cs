using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Queries.Weather.Search;
using WeatherLrt.WebApi.Results;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class WeatherController : WebApiControllerBase<WeatherController>
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator, ILogger<WeatherController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("current/search")]
        public async Task<IActionResult> SearchCurrent([FromQuery] string cityName)
        {
            return await Handle(
                async () =>
                {
                    if (string.IsNullOrWhiteSpace(cityName))
                        return new BadRequestErrorResult("City name has a wrong value");

                    var currentWeather = await _mediator.Send(new SearchCurrentWeatherQuery(cityName));

                    return new OkObjectResult(currentWeather);
                });
        }
    }
}
