using Scraper.Application.Common.Models;

namespace Scraper.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<string> GenerateEmailActivationToken(string userId, CancellationToken cancellationToken);
        Task<JwtDto> LoginAsync(AuthLoginRequest authLoginRequest, CancellationToken cancellationToken);
        Task<JwtDto> SocialLoginAsync(SocialLoginRequest socialLoginRequest, CancellationToken cancellationToken);
        Task<bool> CheckIfUserExist(string userEmail);

    }
}
