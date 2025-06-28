using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollProject1.Support
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
            }

            var chromeDriver= new ChromeDriver(options);
            return new SmartWebDriver(chromeDriver);
        }
    }
}
