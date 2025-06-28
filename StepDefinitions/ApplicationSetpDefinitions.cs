using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using ReqnrollProject1.Pages;
using ReqnrollProject1.Support;
using System.IO;
using System.Threading.Tasks;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public class ApplicationStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _context;

        public ApplicationStepDefinitions(ScenarioContext context)
        {
            _context = context;

            if (context.ContainsKey("driver"))
            {
                _driver = (IWebDriver)context["driver"];
            }
            else
            {
                _driver = DriverFactory.CreateDriver();
                context["driver"] = _driver;
            }
        }

        [Given("I login to the application")]
        public void GivenILoginToTheApplication()
        {
            var url = ConfigReader.GetSection("Login", "Url");
            var username = ConfigReader.GetSection("Login", "Username");
            var password = ConfigReader.GetSection("Login", "Password");

            var loginPage = new LoginPage(_driver);
            loginPage.GoToLoginPage(url);
            loginPage.Login(username, password);
        }

        [When("I select the gym {string}")]
        public void WhenISelectTheGym(string gymUrl)
        {
            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.SwitchToGym(gymUrl);
          //  _driver.Navigate().GoToUrl($"https://{gymUrl}/dashboard");
        }

        [When("I create a package via API")]
        public async Task WhenICreateAPackageViaAPI()
        {
            //navigate to packages Page
            // Generate a unique name with timestamp

            var dashboardPage = new DashboardPage(_driver);

            dashboardPage.NavigateTo("Sales", "Packages");
            string packageName = $"Test API {DateTime.Now:yyyyMMdd-HHmmss}";
            string slug = packageName.ToLower().Replace(" ", "-");

            _context["CreatedPackageName"] = packageName;

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Support\Payloads\CreatePackageTemplate.json");
            string jsonTemplate = File.ReadAllText(templatePath);

            string payload = jsonTemplate
                .Replace("{{packageName}}", packageName)
                .Replace("{{slug}}", slug);


            var api = new RestClientHelper();
            var response = await api.CreatePackageAsync(payload);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode,
    $"API call failed. Status: {response.StatusCode}, Response: {response.Content}");


            var responseJson = JObject.Parse(response.Content);

        }

        [Then("I should see the package created successfully in the application")]
        public void ThenIShouldSeeThePackageCreatedSuccessfullyInTheApplication()
        {
            var packageListPage = new PackageListPage(_driver);
            var packageName = _context["CreatedPackageName"].ToString();
            // Optional: packageListPage.SearchForPackage(packageName);

            bool isVisible = packageListPage.IsPackagePresent(packageName);
            Assert.IsTrue(isVisible, $"Package '{packageName}' was not found in the package list.");

        }


        [AfterScenario]
        public void Cleanup()
        {
            if (_context.ContainsKey("driver"))
            {
                ((IWebDriver)_context["driver"]).Quit();
                _context.Remove("driver");
            }
        }
    }
}
