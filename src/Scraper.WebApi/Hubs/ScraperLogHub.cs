using Microsoft.AspNetCore.SignalR;
using Scraper.Application.Common.Interfaces;
using Scraper.Console;
using System.IdentityModel.Tokens.Jwt;

namespace Scraper.WebApi.Hubs
{
    public class ScraperLogHub : Hub
    {
        private readonly IAuthenticationService _authenticationService;

        public ScraperLogHub(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override async Task OnConnectedAsync()
        {
            
            string accessToken = Context.GetHttpContext().Request.Query["access_token"];
           
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var email = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            // AccessToken doğrulama işlemleri (Yetkilendirme servisini kullanabilirsiniz)
            if (await _authenticationService.CheckIfUserExist(email))
            {
                await base.OnConnectedAsync();
            }
            else
            {
                // Yetkilendirme başarısızsa, bağlantıyı kapatın
                Context.Abort();
            }
        }
        public async Task SendLogAsync(ScraperLogDto log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewScraperLogAdded", log);
        }
    }
}
