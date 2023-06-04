namespace Scraper.Console
{
    public class ProductDto
    {
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public string Price { get; set; }

        public string? SalePrice { get; set; }
    }
}
