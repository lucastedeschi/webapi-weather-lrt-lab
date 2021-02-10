using System.Collections.Generic;
using FluentValidation.Results;
using WeatherLrt.Application.Commands.Common;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignIn
{
    public sealed class SignInApplicationUserCommandResponse : CommandResponseBase
    {
        public SignInApplicationUserCommandResponse(string token)
        {
            Token = token;
        }

        public SignInApplicationUserCommandResponse(IEnumerable<ValidationFailure> validations) : base(validations)
        {
        }

        public SignInApplicationUserCommandResponse(IEnumerable<string> errors) : base(errors)
        {
        }

        public string Token { get; }
    }
}
