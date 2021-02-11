using System.Collections.Generic;
using FluentValidation.Results;
using WeatherLrt.Application.Commands.Common;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignUp
{
    public sealed class SignUpApplicationUserCommandResponse : CommandResponseBase
    {
        public SignUpApplicationUserCommandResponse(string applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public SignUpApplicationUserCommandResponse(IEnumerable<ValidationFailure> validations) : base(validations)
        {
        }

        public SignUpApplicationUserCommandResponse(IEnumerable<string> errors) : base(errors)
        {
        }

        public string ApplicationUserId { get; }
    }
}
