using Scraper.Application.Common.Models;

namespace Scraper.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<string> GenerateEmailActivationToken(string userId, CancellationToken cancellationToken);
    }
}
