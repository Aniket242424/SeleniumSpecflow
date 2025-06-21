using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public class CalculatorStepDefinitions
    {
        private IWebDriver driver;
        private int firstNumber;
        private int secondNumber;
        private int result;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // This ChromeDriver will work in GitHub Actions after setting up chromedriver
            var options = new ChromeOptions();
            options.AddArgument("--headless"); // Headless for CI
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            driver = new ChromeDriver(options); // No path passed — uses GitHub-installed chromedriver
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }

        [Given("the first number is {int}")]
        public void GivenTheFirstNumberIs(int number)
        {
            firstNumber = number;
        }

        [Given("the second number is {int}")]
        public void GivenTheSecondNumberIs(int number)
        {
            secondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            result = firstNumber + secondNumber;
        }

        [Then("the result should be {int}")]
        public void ThenTheResultShouldBe(int expected)
        {
            Assert.AreEqual(expected, result);
        }

        [Given("I navigate to the google page")]
        public void GivenINavigateToGooglePage()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        [When("I enter search results")]
        public void WhenIEnterSearchResults()
        {
            var searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium automation");
            searchBox.Submit();
        }

        [Then("I should see the search results")]
        public void ThenIShouldSeeTheSearchResults()
        {
            var results = driver.FindElements(By.CssSelector("div.g"));
            Assert.IsTrue(results.Count > 0);
        }
    }
}
