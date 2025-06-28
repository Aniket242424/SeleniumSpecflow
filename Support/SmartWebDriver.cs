using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;

namespace ReqnrollProject1.Support
{
    public class SmartWebDriver : IWebDriver
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SmartWebDriver(IWebDriver driver, int timeoutInSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public IWebElement FindElement(By by)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        // Delegate all other IWebDriver methods to _driver
        public void Dispose() => _driver.Dispose();
        public void Close() => _driver.Close();
        public void Quit() => _driver.Quit();
        public IOptions Manage() => _driver.Manage();
        public INavigation Navigate() => _driver.Navigate();
        public ITargetLocator SwitchTo() => _driver.SwitchTo();
        public string Url { get => _driver.Url; set => _driver.Url = value; }
        public string Title => _driver.Title;
        public string PageSource => _driver.PageSource;
        public string CurrentWindowHandle => _driver.CurrentWindowHandle;
        public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _wait.Until(drv =>
            {
                var elements = drv.FindElements(by);
                return elements.Count > 0 ? elements : null;
            });
        }



    }


    public static class WebDriverExtensions
    {
        public static void JsClick(this IWebDriver driver, IWebElement element)
        {
            if (driver is IJavaScriptExecutor js)
            {
                js.ExecuteScript("arguments[0].click();", element);
            }
            else
            {
                throw new InvalidOperationException("Driver does not support JavaScript execution.");
            }
        }

        public static void SafeClick(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by).Click();
            }
            catch (ElementClickInterceptedException)
            {
                driver.JsClick(driver.FindElement(by));
            }
        }


    }
}
