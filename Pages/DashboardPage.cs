using OpenQA.Selenium;

namespace ReqnrollProject1.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;

        private IWebElement adminControlPanel => _driver.FindElement(By.XPath("//span[text()='Admin Control Panel']"));

        private IWebElement searchGymInput => _driver.FindElement(By.XPath("//input[@data-cy='search-for-gym']"));

        private IWebElement gymNameLink => _driver.FindElement(By.XPath("//a[contains(text(),'Aniket')]"));


        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Assumption: Package name appears somewhere on the page (update selector as needed)
        public bool IsPackageVisible(string packageName)
        {
            try
            {
                var bodyText = _driver.FindElement(By.TagName("body")).Text;
                return bodyText.Contains(packageName);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Navigate to Gym Dashbaord
        public void SwitchToGym(string gymName)
        {
            adminControlPanel.Click();
            searchGymInput.SendKeys(gymName);
            GetGymHost(gymName).Click();
            gymNameLink.Click();
        }


        // Optional: Navigate directly to package list page if not already on dashboard
        public void GoTo(string gymDashboardUrl)
        {
            _driver.Navigate().GoToUrl(gymDashboardUrl);
        }

        public IWebElement GetGymHost(string gymName)
        {

            return _driver.FindElement(By.XPath($"//span[normalize-space()='{gymName}']"));

        }

        public IWebElement GetGymName(string gymName) {

            return _driver.FindElement(By.XPath($"//a[normalize-space()='{gymName}']"));

        }

        public void NavigateTo(String Menu , String SubMenu)
        {
            _driver.FindElement(By.XPath($"//span[text()='{Menu}']")).Click();

            _driver.FindElement(By.XPath($"//a[text()='{SubMenu}']")).Click();
        }
    }
}
