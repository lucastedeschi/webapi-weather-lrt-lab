using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Commands.ApplicationUsers.SignIn;
using WeatherLrt.Application.Commands.ApplicationUsers.SignUp;
using WeatherLrt.WebApi.Results;

namespace WeatherLrt.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class LoginController : WebApiControllerBase<LoginController>
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator, ILogger<LoginController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInApplicationUserCommand command)
        {
            return await Handle(
                async () =>
                {
                    if (command is null)
                        return new BadRequestErrorResult("Request body must not be empty");

                    var response = await _mediator.Send(command);

                    return new OkObjectResult(response);
                });
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpApplicationUserCommand command)
        {
            return await Handle(
                async () =>
                {
                    if (command is null)
                        return new BadRequestErrorResult("Request body must not be empty");

                    var response = await _mediator.Send(command);

                    return new OkObjectResult(response);
                });
        }
    }
}
