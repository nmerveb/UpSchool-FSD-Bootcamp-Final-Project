namespace Scraper.Console
{
    public class ResponseDto
    {
        public List<ProductDto> Products { get; set; }

        public List<OrderEventDto> OrderEvents { get; set; }

        public int TotalProductAmount { get; set; }

        public ResponseDto(List<ProductDto> products, List<OrderEventDto> orderEvents, int totalProductAmount)
        {
            Products = products;
            OrderEvents = orderEvents;
            TotalProductAmount = totalProductAmount;

        }
    }
}
