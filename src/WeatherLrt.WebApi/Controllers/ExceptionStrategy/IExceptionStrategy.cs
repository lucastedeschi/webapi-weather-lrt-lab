using System;
using Microsoft.AspNetCore.Mvc;

namespace WeatherLrt.WebApi.Controllers.ExceptionStrategy
{
    internal interface IExceptionStrategy
    {
        IActionResult GetResult(Exception exception);
    }
}
