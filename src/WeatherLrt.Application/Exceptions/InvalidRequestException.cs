using System;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Application.Exceptions
{
    public sealed class InvalidRequestException : Exception, ICustomException
    {
        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}
