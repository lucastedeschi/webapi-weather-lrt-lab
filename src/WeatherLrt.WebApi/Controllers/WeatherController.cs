using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("takeAnUmbrella")]
        public async Task<IActionResult> TakeAnUmbrella([FromQuery] string city)
        {
            return await Handle(
                async () =>
                {
                    if (string.IsNullOrWhiteSpace(city))
                        return new BadRequestErrorResult("City has a wrong value");

                    return default;
                });
        }
    }
}
