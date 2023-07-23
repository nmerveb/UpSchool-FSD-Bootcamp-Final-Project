using HtmlAgilityPack;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraper.Console;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Scraper.Application.Utils
{
    public class Crawler
    {
        private static string SCRAPING_PAGE = "https://4teker.net/";

        private readonly HubConnection _connection;
        public Crawler(string accessToken)
        {

            _connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7287/Hubs/ScraperLogHub?access_token={accessToken}")
                .WithAutomaticReconnect()
                .Build();

        }
        public async Task<ResponseDto> ScrapProducts(Guid orderId)
        {
            _connection.StartAsync();

            new DriverManager().SetUpDriver(new ChromeConfig());
            IWebDriver driver = new ChromeDriver();

            var totalProductAmount = 0;

            List<ProductDto> productList = new List<ProductDto>();
            List<OrderEventDto> orderEventList = new List<OrderEventDto>();

            orderEventList.Add(new OrderEventDto(orderId, OrderStatus.BotStarted, DateTimeOffset.Now));
            await _connection.SendAsync("SendLogAsync", CreateLog("--Bot Started--"));


            driver.Navigate().GoToUrl("https://4teker.net/");
            Thread.Sleep(2000);


            var activePage = int.Parse(driver.FindElement(By.CssSelector(".page-link.active.page-number")).Text);
            var totalPageCount = driver.FindElements(By.CssSelector(".page-link.page-number")).Count() + 1;

            orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingStarted, DateTimeOffset.Now));
            await _connection.SendAsync("SendLogAsync", CreateLog("--Scraping Started--"));
            try
            {
                while (activePage < totalPageCount)
                {
                    Thread.Sleep(2000);
                    driver.Navigate().GoToUrl($"https://4teker.net/?currentPage={activePage}");
                    IReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".card.h-100"));

                    foreach (var product in products)
                    {
                        string htmlText = product.GetAttribute("innerHTML");

                        HtmlDocument doc = new HtmlDocument();

                        doc.LoadHtml(htmlText);

                        //Get products properties
                        var name = doc.DocumentNode.SelectSingleNode("//h5[@class='fw-bolder product-name']")?.InnerText;
                        var picture = $"https://4teker.net/{doc.DocumentNode.SelectSingleNode("//img")?.GetAttributeValue("src", "")}";
                        var isOnSale = doc.DocumentNode.SelectSingleNode("//div[@class='badge bg-dark text-white position-absolute onsale']") != null;
                        var price = doc.DocumentNode.SelectSingleNode("//span[@class='text-muted text-decoration-line-through price']")?.InnerText;
                        if (price == null)
                        {
                            price = doc.DocumentNode.SelectSingleNode("//span[@class='price']").InnerText;
                        }
                        var salePrice = doc.DocumentNode.SelectSingleNode("//span[@class='sale-price']")?.InnerText;

                        //Create new product
                        var newPorduct = new ProductDto()
                        {
                            OrderId = orderId,
                            Name = name,
                            Picture = picture,
                            IsOnSale = isOnSale,
                            Price = price,
                            SalePrice = salePrice
                        };

                        productList.Add(newPorduct);

                        totalProductAmount++;
                    }

                    await _connection.SendAsync("SendLogAsync", CreateLog($"Page {activePage} scraped.Total product count:{totalProductAmount}."));

                    activePage++;
                }

                orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingCompleted, DateTimeOffset.Now));
                await _connection.SendAsync("SendLogAsync", CreateLog("--Scraping Completed--"));
            }
            catch
            {
                orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingFailed, DateTimeOffset.Now));
                await _connection.SendAsync("SendLogAsync", CreateLog("--Scraping Failed--"));
            }




            driver.Close();
            orderEventList.Add(new OrderEventDto(orderId, OrderStatus.OrderCompleted, DateTimeOffset.Now));
            await _connection.SendAsync("SendLogAsync", CreateLog("--Order Completed--"));

            ResponseDto response = new(productList, orderEventList, totalProductAmount);

            return response;

        }

        ScraperLogDto CreateLog(string message) => new ScraperLogDto(message);
    }
}
