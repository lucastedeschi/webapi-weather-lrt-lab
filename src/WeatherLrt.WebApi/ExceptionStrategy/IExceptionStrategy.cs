using System;
using Microsoft.AspNetCore.Mvc;

namespace WeatherLrt.WebApi.ExceptionStrategy
{
    internal interface IExceptionStrategy
    {
        IActionResult GetResult(Exception exception);
    }
}
