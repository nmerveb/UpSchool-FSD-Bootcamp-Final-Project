using MediatR;
using Scraper.Application.Common.Interfaces;
using Scraper.Application.Common.Models;

namespace Scraper.Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, AuthRegisterDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthRegisterCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<AuthRegisterDto> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
            var createUserDto = new CreateUserDto(request.FirstName,request.LastName, request.Email);

            var userId = await _authenticationService.CreateUserAsync(createUserDto, cancellationToken);

            var emailToken = await _authenticationService.GenerateEmailActivationToken(userId, cancellationToken);

            var fullName = $"{request.FirstName} {request.LastName}";

            return new AuthRegisterDto(request.Email, fullName, emailToken);
        }
    }
}
