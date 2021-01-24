using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherLrt.Domain.Models;

namespace WeatherLrt.WebApi.Results
{
    public sealed class BadRequestErrorResult : BadRequestObjectResult
    {
        public BadRequestErrorResult(string error) : base(new ErrorResult(error))
        {
        }

        public BadRequestErrorResult(IEnumerable<string> errors) : base(new ErrorResult(errors))
        {
        }
    }
}
