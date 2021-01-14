using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherLrt.WebApi.Controllers.ExceptionStrategy
{
    internal sealed class CustomExceptionStrategy<TLogger> : IExceptionStrategy
    {
        private readonly ILogger<TLogger> _logger;

        public CustomExceptionStrategy(ILogger<TLogger> logger)
        {
            _logger = logger;
        }

        public IActionResult GetResult(Exception exception)
        {
            _logger.LogInformation(exception.ToString());

            return new BadRequestObjectResult(exception.Message);
        }
    }
}
