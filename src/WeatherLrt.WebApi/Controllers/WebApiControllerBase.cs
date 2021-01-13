using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherLrt.WebApi.Controllers
{
    public abstract class WebApiControllerBase<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;

        public WebApiControllerBase(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> Handle(Func<Task<IActionResult>> body)
        {
            try
            {
                return await body();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());

                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
