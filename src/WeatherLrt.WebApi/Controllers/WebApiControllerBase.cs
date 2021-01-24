using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLrt.Application.Interfaces;
using WeatherLrt.WebApi.Controllers.ExceptionStrategy;

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
                IExceptionStrategy exceptionStrategy = ex switch
                {
                    ICustomException _ => new CustomExceptionStrategy<T>(_logger),
                    _ => new UnexcpectedExceptionStrategy<T>(_logger)
                };

                return exceptionStrategy.GetResult(ex);
            }
        }
    }
}
