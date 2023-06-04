namespace Scraper.Console
{
    public class ScraperLogDto
    {
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ConnectionId { get; set; }

        public ScraperLogDto(string message, string connectionId)
        {
            Message = message;
            CreatedOn = DateTime.Now;
            ConnectionId = connectionId;

        }
    }
}
