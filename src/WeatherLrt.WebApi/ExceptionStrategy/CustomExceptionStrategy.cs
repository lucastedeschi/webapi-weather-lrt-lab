using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.WebApi.Results;

namespace WeatherLrt.WebApi.ExceptionStrategy
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

            return new BadRequestErrorResult(exception.Message);
        }
    }
}
