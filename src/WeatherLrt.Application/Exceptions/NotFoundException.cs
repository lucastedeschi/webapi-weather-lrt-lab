using System;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Application.Exceptions
{
    public sealed class NotFoundException : Exception, ICustomException
    {
        public NotFoundException(string name, object id) : base($"\"{name}\" ({id}) not found")
        {
        }
    }
}
