using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesProject.e2eTests
{
    public class e2eTests : IDisposable
    {
        public static IWebDriver webDriver;

        private static string baseUrl = "https://localhost:5001/";

        public e2eTests()
        {
           if (OperatingSystem.IsMacOS())
           {
               webDriver = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: @"/Users/user/Desktop/Test Files/Meetup/dotnet-core-testing/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.e2eTests/WebDrivers/MacOS");
           }
           if (OperatingSystem.isLinux())
           {
               webDriver = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: @"/Users/user/Desktop/Test Files/Meetup/dotnet-core-testing/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.e2eTests/WebDrivers/Linux");
           }
           if (OperatingSystem.isLinux())
           {
               webDriver = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: @"/Users/user/Desktop/Test Files/Meetup/dotnet-core-testing/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.e2eTests/WebDrivers/Windows/");
           }
        }

        public void Dispose()
        {
            webDriver.Dispose();
        }

        [Fact]
        public void TestWithFirefoxDriver()
        {

           webDriver.Navigate().GoToUrl(baseUrl);

           webDriver.Quit();

        }
    }
}