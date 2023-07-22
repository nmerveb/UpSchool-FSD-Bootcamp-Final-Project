using MediatR;

namespace Scraper.Application.Features.Auth.Commands.Login
{
    public class AuthLoginCommand:IRequest<AuthLoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
