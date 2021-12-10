using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp2
{
    public class Program1
    {
        static async Task Main(string[] args)
        {
            var options = new ChromeOptions();

            var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));

            NetworkAuthenticationHandler handler = new NetworkAuthenticationHandler()
            {
                UriMatcher = d => d.Host.Contains("jigsaw.w3.org"),
                Credentials = new PasswordCredentials("guest", "guest")
            };

            var networkInterceptor = driver.Manage().Network;
            networkInterceptor.AddAuthenticationHandler(handler);
            await networkInterceptor.StartMonitoring();

            driver.Navigate().GoToUrl("https://jigsaw.w3.org/HTTP/Basic/");

            await networkInterceptor.StopMonitoring();
            driver.Quit();
        }
    }
}
