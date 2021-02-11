using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignUp
{
    public sealed class SignUpApplicationUserCommandHandler : IRequestHandler<SignUpApplicationUserCommand, SignUpApplicationUserCommandResponse>
    {
        private readonly IApplicationUserIdentityService _applicationUserIdentityService;

        public SignUpApplicationUserCommandHandler(IApplicationUserIdentityService applicationUserIdentityService)
        {
            _applicationUserIdentityService = applicationUserIdentityService;
        }

        public async Task<SignUpApplicationUserCommandResponse> Handle(SignUpApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new SignUpApplicationUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new SignUpApplicationUserCommandResponse(validationResult.Errors);

            var (signedUp, applicationUserId, errorMessage) = await _applicationUserIdentityService.SignUp(request.UserName, request.Email, request.Password);
            if (!signedUp)
                return new SignUpApplicationUserCommandResponse(new List<string> { errorMessage });

            return new SignUpApplicationUserCommandResponse(applicationUserId);
        }
    }
}
