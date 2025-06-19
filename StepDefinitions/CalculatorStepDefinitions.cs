using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Reqnroll;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private IWebDriver driver;

        // Before each scenario, instantiate the WebDriver
        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();  // Initialize WebDriver
        }

        [Given(@"I navigate to the google page")]
        public void GivenINavigateToTheGooglePage()
        {
            driver.Navigate().GoToUrl("https://google.com");
        }

        [When(@"I enter search results")]
        public void WhenIEnterSearchResults()
        {
            driver.FindElement(By.Name("q")).SendKeys("validUser");
            driver.FindElement(By.Name("btnK")).Click(); // Google Search button
        }

        [Then(@"I should see the search results")]
        public void ThenIShouldSeeTheSearchResults()
        {
            var firstResult = driver.FindElement(By.CssSelector("h3")).Text;
            Assert.IsTrue(firstResult.Contains("Google"));
            driver.Quit();  // Clean up after test
        }
    }
}
