using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


IWebDriver driver = new ChromeDriver();
try
{
    //Bu kisim signalR/api ile frontend'den alinmis olacak.
    var totalCountOfProduct = 0;
    var requestCountOfProduct = 0;
    var requestTypeOfProduct = 0;
    
    driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/");
    Thread.Sleep(2000);
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("------------------------");
    Console.WriteLine("Logged into the web page.");
    Console.WriteLine("------------------------");
    Console.ResetColor();

    var activePage = int.Parse(driver.FindElement(By.CssSelector(".page-link.active.page-number")).Text);
    var totalPageCount = driver.FindElements(By.CssSelector(".page-link.page-number")).Count()+1;
    Console.WriteLine(totalPageCount.ToString());
    Console.WriteLine(activePage.ToString());
   
    while (activePage < totalPageCount)
    {
        Thread.Sleep(2000);
        driver.Navigate().GoToUrl($"https://finalproject.dotnet.gg/?currentPage={activePage}");
        IReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".card.h-100"));
        foreach (var product in products)
        {
            string test = product.Text;
            //Console.WriteLine(test);
            totalCountOfProduct++;
        }

        Console.WriteLine($"Page {activePage} scraped.Total product count:{totalCountOfProduct}. -- {DateTime.Now}");
        activePage++;
    }

    driver.Close();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("------------------------");
    Console.WriteLine("Scraping completed.");
    Console.WriteLine("------------------------");
    Console.ResetColor();
}
catch
{

}
