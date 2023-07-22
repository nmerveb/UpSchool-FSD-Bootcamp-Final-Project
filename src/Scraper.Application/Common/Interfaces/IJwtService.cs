using Scraper.Application.Common.Models;

namespace Scraper.Application.Common.Interfaces
{
    public interface IJwtService
    {
        JwtDto Generate(string userId,string email,string firstName, string lastName,  List<string>? roles = null);
    }
}
