﻿using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace WeatherLrt.Application.Commands.Common
{
    public abstract class CommandResponseBase
    {
        public CommandResponseBase()
        {
            Errors = new List<string>();
        }

        protected CommandResponseBase(IEnumerable<ValidationFailure> validations)
        {
            Errors = validations.Select(v => v.ErrorMessage);
        }

        protected CommandResponseBase(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
