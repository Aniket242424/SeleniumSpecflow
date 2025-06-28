using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ReqnrollProject1.Support;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public class CalculatorStepDefinitions
    {
        private readonly IWebDriver _driver;
        private int firstNumber, secondNumber, result;

        public CalculatorStepDefinitions(ScenarioContext context)
        {
            _driver = context.ContainsKey("driver")
                ? (IWebDriver)context["driver"]
                : DriverFactory.CreateDriver();

            context["driver"] = _driver;
        }

        [Given("the first number is {int}")]
        public void GivenTheFirstNumberIs(int number) => firstNumber = number;

        [Given("the second number is {int}")]
        public void GivenTheSecondNumberIs(int number) => secondNumber = number;

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded() => result = firstNumber + secondNumber;

        [Then("the result should be {int}")]
        public void ThenTheResultShouldBe(int expected) => Assert.AreEqual(expected, result);

        [Given("I navigate to the google page")]
        public void GivenINavigateToGooglePage() => _driver.Navigate().GoToUrl("https://www.google.com");

        [When("I enter search results")]
        public void WhenIEnterSearchResults()
        {
            var searchBox = _driver.FindElement(By.Name("q"));
            searchBox.SendKeys("Selenium automation");
            searchBox.Submit();
        }

        [Then("I should see the search results")]
        public void ThenIShouldSeeTheSearchResults()
        {
            var results = _driver.FindElements(By.CssSelector("div.g"));
            Assert.IsTrue(results.Count > 0);
        }
    }
}