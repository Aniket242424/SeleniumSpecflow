using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProject1.Support
{
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["WebDriver"]; // make sure driver is stored here in BeforeScenario
        }

        [AfterScenario]
        public void TakeScreenshotOnFailure()
        {
            if (_scenarioContext.TestError != null)
            {
                string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots", _scenarioContext.ScenarioInfo.Title);
                Directory.CreateDirectory(screenshotsDir); // ensure folder exists

                string fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string fullPath = Path.Combine(screenshotsDir, fileName);

                Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);


                Console.WriteLine($"Screenshot saved to: {fullPath}");
            }
        }

    }
}
