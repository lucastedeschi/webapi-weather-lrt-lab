using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class WeatherController : WebApiControllerBase<WeatherController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IMediator mediator, ILogger<WeatherController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("takeAnUmbrella")]
        public async Task<IActionResult> TakeAnUmbrella([FromQuery] string city)
        {
            return await Handle(
                async () =>
                {
                    if (string.IsNullOrWhiteSpace(city))
                        return new BadRequestObjectResult("City has a wrong value");

                    return default;
                });
        }
    }
}
