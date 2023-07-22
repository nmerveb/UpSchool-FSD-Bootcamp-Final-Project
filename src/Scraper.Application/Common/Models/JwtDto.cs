namespace Scraper.Application.Common.Models
{
    public class JwtDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiryDate { get; set; }

        public JwtDto(string accessToken, DateTime expiryDate)
        {
            AccessToken = accessToken;
            ExpiryDate = expiryDate;
        }
    }
}
