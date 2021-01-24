using System.Collections.Generic;
using FluentValidation.Results;
using WeatherLrt.Application.Commands.Common;

namespace WeatherLrt.Application.Commands.SystemUsers.Create
{
    public sealed class CreateSystemUserCommandResponse : CommandResponseBase
    {
        public CreateSystemUserCommandResponse(long systemUserId)
        {
            SystemUserId = systemUserId;
        }

        public CreateSystemUserCommandResponse(IEnumerable<ValidationFailure> validations) : base(validations)
        {
        }

        public long SystemUserId { get; }
    }
}
