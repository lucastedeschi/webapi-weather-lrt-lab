using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Queries.SystemUsers.Get;
using WeatherLrt.Application.Queries.SystemUsers.Search;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SystemUserController : WebApiControllerBase<SystemUserController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SystemUserController> _logger;

        public SystemUserController(IMediator mediator, ILogger<SystemUserController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] long systemUserId)
        {
            return await Handle(
                async () => 
                {
                    if (systemUserId != default)
                        return new BadRequestObjectResult("Id must not be empty");

                    var systemUser = await _mediator.Send(new GetSystemUserQuery(systemUserId));

                    return new OkObjectResult(systemUser);
                });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name = null, [FromQuery] string email = null)
        {
            return await Handle(
                async () =>
                {
                    if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(email))
                        return new BadRequestObjectResult("Name and / or Email must not be empty");

                    var systemUsers = await _mediator.Send(new SearchSystemUserQuery(name, email));

                    return new OkObjectResult(systemUsers);
                });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSystemUserCommand command)
        {
            return await Handle(
                async () =>
                {
                    if (command != null)
                        return new BadRequestObjectResult("Command has a wrong value");

                    var systemUserId = await _mediator.Send(command);

                    var systemUser = await _mediator.Send(new GetSystemUserQuery(systemUserId));

                    return new OkObjectResult(systemUser);
                });
        }
    }
}
