using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollProject1.Support
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(bool headless = false)
        {
            var options = new ChromeOptions();

            // Always use a unique user data directory
            string userDataDir = $"/tmp/chrome-profile-{Guid.NewGuid()}";
            options.AddArgument($"--user-data-dir={userDataDir}");

            // Set a unique debugging port for parallel tests
            var randomPort = new Random().Next(5000, 65000);
            options.AddArgument($"--remote-debugging-port={randomPort}");

            // Add common arguments
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");

            if (headless || Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
            {
                options.AddArgument("--headless=new");
            }

            var chromeDriver = new ChromeDriver(options);
            return new SmartWebDriver(chromeDriver);
        }
    }
}
