using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RazorPagesProject.e2eTests.Pages;

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
           if (OperatingSystem.IsLinux())
           {
               webDriver = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: @"/Users/user/Desktop/Test Files/Meetup/dotnet-core-testing/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.e2eTests/WebDrivers/Linux");
           }
           if (OperatingSystem.IsWindows())
           {
               webDriver = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: @"/Users/user/Desktop/Test Files/Meetup/dotnet-core-testing/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.e2eTests/WebDrivers/Windows/");
           }
        }
        public void Dispose()
        {
            webDriver.Dispose();
        }

        [Fact]
        public void TestWithChromeDriver()
        {

           webDriver.Navigate().GoToUrl(baseUrl);

           var homePage = new HomePage(webDriver);

           var message = "This is a test";

           Thread.Sleep(5000);

           homePage.AddMessage(message);

           Thread.Sleep(10000);

           Assert.True(homePage.IsMessageAppearToList(message));

           webDriver.Quit();

        }
    }
}