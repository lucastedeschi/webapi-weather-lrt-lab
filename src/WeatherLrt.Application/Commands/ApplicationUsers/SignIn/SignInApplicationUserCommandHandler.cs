using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Application.Commands.ApplicationUsers.SignIn
{
    public sealed class SignInApplicationUserCommandHandler : IRequestHandler<SignInApplicationUserCommand, SignInApplicationUserCommandResponse>
    {
        private readonly IApplicationUserIdentityService _applicationUserIdentityService;

        public SignInApplicationUserCommandHandler(IApplicationUserIdentityService applicationUserIdentityService)
        {
            _applicationUserIdentityService = applicationUserIdentityService;
        }

        public async Task<SignInApplicationUserCommandResponse> Handle(SignInApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new SignInApplicationUserCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new SignInApplicationUserCommandResponse(validationResult.Errors);

            var (signedIn, token, errorMessage) = await _applicationUserIdentityService.SignIn(request.Email, request.Password);
            if (!signedIn)
                return new SignInApplicationUserCommandResponse(new List<string> { errorMessage });

            return new SignInApplicationUserCommandResponse(token);
        }
    }
}
