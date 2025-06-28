using OpenQA.Selenium;

namespace ReqnrollProject1.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Elements
        private IWebElement Email => _driver.FindElement(By.Name("_username"));
        private IWebElement Password => _driver.FindElement(By.Name("_password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector("button[data-cy='login-submit']"));

        // Actions
        public void GoToLoginPage(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
        }

        public void Login(string username, string password)
        {
            Email.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();
        }
    }
}
