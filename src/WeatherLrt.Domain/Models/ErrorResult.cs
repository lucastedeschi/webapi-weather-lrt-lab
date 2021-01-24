using System.Collections.Generic;

namespace WeatherLrt.Domain.Models
{
    public class ErrorResult
    {
        public ErrorResult()
        {
        }

        public ErrorResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ErrorResult(string error) : this(new List<string> { error })
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
