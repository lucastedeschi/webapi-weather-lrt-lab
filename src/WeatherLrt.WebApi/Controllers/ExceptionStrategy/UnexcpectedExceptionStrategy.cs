using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherLrt.WebApi.Controllers.ExceptionStrategy
{
    public class UnexcpectedExceptionStrategy<TLogger> : IExceptionStrategy
    {
        private readonly ILogger<TLogger> _logger;

        public UnexcpectedExceptionStrategy(ILogger<TLogger> logger)
        {
            _logger = logger;
        }

        public IActionResult GetResult(Exception exception)
        {
            _logger.LogError(exception.ToString());

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
