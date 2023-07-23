using HtmlAgilityPack;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scraper.Console;
using System.Net.Http.Json;

public class ScraperConsole
{
    IWebDriver driver = new ChromeDriver();
    const string connectionUrl = "https://localhost:7287/Hubs/ScraperLogHub";
    public async Task<ResponseDto> ScrapProducts(Guid orderId)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(connectionUrl)
            .WithAutomaticReconnect()
            .Build();

        await hubConnection.StartAsync();

        var httpClient = new HttpClient();

        await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Bot started.-----", hubConnection.ConnectionId));

        var totalProductAmount = 0;
        
        List<ProductDto> productList = new List<ProductDto>();
        List<OrderEventDto> orderEventList = new List<OrderEventDto>();

        orderEventList.Add(new OrderEventDto(orderId, OrderStatus.BotStarted, DateTimeOffset.Now));

        driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/");
        Thread.Sleep(2000);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("------------------------");
        Console.WriteLine("Logged into the web page.");
        Console.WriteLine("------------------------");
        Console.ResetColor();
        await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Logged into the web page.-----", hubConnection.ConnectionId));

        var activePage = int.Parse(driver.FindElement(By.CssSelector(".page-link.active.page-number")).Text);
        var totalPageCount = driver.FindElements(By.CssSelector(".page-link.page-number")).Count() + 1;

        orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingStarted, DateTimeOffset.Now));
        await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Scraping started.-----", hubConnection.ConnectionId));

        try
        {
            while (activePage < totalPageCount)
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={activePage}");
                IReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".card.h-100"));

                foreach (var product in products)
                {
                    string htmlText = product.GetAttribute("innerHTML");

                    HtmlDocument doc = new HtmlDocument();

                    doc.LoadHtml(htmlText);

                    //Get products properties
                    var name = doc.DocumentNode.SelectSingleNode("//h5[@class='fw-bolder product-name']")?.InnerText;
                    var picture = $"https://finalproject.dotnet.gg/{doc.DocumentNode.SelectSingleNode("//img")?.GetAttributeValue("src", "")}";
                    var isOnSale = doc.DocumentNode.SelectSingleNode("//div[@class='badge bg-dark text-white position-absolute onsale']") != null;
                    var price = doc.DocumentNode.SelectSingleNode("//span[@class='text-muted text-decoration-line-through price']")?.InnerText;
                    if(price == null)
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

                Console.WriteLine($"Page {activePage} scraped.Total product count:{totalProductAmount}. -- {DateTime.Now}");
                await httpClient.PostAsJsonAsync(connectionUrl, CreateLog($"Page {activePage} scraped.Total product count:{totalProductAmount}.", hubConnection.ConnectionId));

                activePage++;
            }

            orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingCompleted, DateTimeOffset.Now));
            await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Scraping completed.-----", hubConnection.ConnectionId));
        }
        catch
        {
            orderEventList.Add(new OrderEventDto(orderId, OrderStatus.ScrapingFailed, DateTimeOffset.Now));
            await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Scraping failed.-----", hubConnection.ConnectionId));
        }


        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("------------------------");
        Console.WriteLine("Scraping completed.");
        Console.WriteLine("------------------------");
        Console.ResetColor();

        driver.Close();
        orderEventList.Add(new OrderEventDto(orderId, OrderStatus.OrderCompleted, DateTimeOffset.Now));
        await httpClient.PostAsJsonAsync(connectionUrl, CreateLog("---- Order completed.-----", hubConnection.ConnectionId));

        //ResponseDto created
        ResponseDto response = new  (productList, orderEventList, totalProductAmount);

        return response;

    }

    ScraperLogDto CreateLog(string message, string connectionId) => new ScraperLogDto(message);

}



