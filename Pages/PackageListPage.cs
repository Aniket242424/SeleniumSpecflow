using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace ReqnrollProject1.Pages
{
    public class PackageListPage
    {
        private readonly IWebDriver _driver;

        public PackageListPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Replace this selector with the actual list container or rows
        private IReadOnlyCollection<IWebElement> PackageElements => _driver.FindElements(By.XPath("//td[contains(@class,'bold')]/div/span"));

        public bool IsPackagePresent(string packageName)
        {
            _driver.Navigate().Refresh();
            return PackageElements.Any(pkg => pkg.Text.Trim().Equals(packageName.Trim()));
        }

        // Optional: If there's a search box
        public void SearchForPackage(string packageName)
        {
            var searchBox = _driver.FindElement(By.CssSelector("input[name='search']")); // replace selector
            searchBox.Clear();
            searchBox.SendKeys(packageName);
            searchBox.SendKeys(Keys.Enter);
        }
    }
}
