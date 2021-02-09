using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Commands.SystemUsers.Create;
using WeatherLrt.Application.Queries.SystemUsers.Get;
using WeatherLrt.Application.Queries.SystemUsers.Search;
using WeatherLrt.WebApi.Results;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SystemUserController : WebApiControllerBase<SystemUserController>
    {
        private readonly IMediator _mediator;

        public SystemUserController(IMediator mediator, ILogger<SystemUserController> logger) : base(logger)
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

                    var systemUser = await _mediator.Send(new GetSystemUserQuery(id));

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
                        return new BadRequestErrorResult("Name and / or Email must not be empty");

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
                    if (command is null)
                        return new BadRequestErrorResult("Command has a wrong value");

                    var response = await _mediator.Send(command);

                    if (response.Errors.Any())
                        return new BadRequestErrorResult(response.Errors);

                    var systemUser = await _mediator.Send(new GetSystemUserQuery(response.SystemUserId));

                    return new OkObjectResult(systemUser);
                });
        }
    }
}
