namespace Scraper.Console
{
    public class ScraperLogDto
    {
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public ScraperLogDto(string message)
        {
            Message = message;
            CreatedOn = DateTime.Now;
            
        }
    }
}
