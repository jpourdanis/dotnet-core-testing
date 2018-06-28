using Xunit;
using OpenQA.Selenium;
using System;
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
            webDriver.Quit();
            webDriver.Dispose();
        }

        [Fact]
        public void CanAddMessageToListAndVerify()
        {

            webDriver.Navigate().GoToUrl(baseUrl);

            var homePage = new HomePage(webDriver);

            var message = "This is a test";

            Thread.Sleep(5000);

            homePage.AddMessage(message);

            Thread.Sleep(10000);

            Assert.True(homePage.IsMessageAppearToList(message));

        }

        [Fact]
        public void CanDeleteAllMessagesAndVerify()
        {

            webDriver.Navigate().GoToUrl(baseUrl);

            var homePage = new HomePage(webDriver);

            Thread.Sleep(5000);

            homePage.DeleteAllMessages();

            Thread.Sleep(10000);

            Assert.True(homePage.IsMessagesListEmpty());

        }

        [Theory]
        [InlineData("ypourdanis")]
        [InlineData("pavkout")]
        [InlineData("ziaziosk")]
        public void SearchGitHubProfile(string username)
        {
            webDriver.Navigate().GoToUrl(baseUrl);

            var gitHubPage = new GitHubPage(webDriver);

            Thread.Sleep(5000);

            gitHubPage.SearchForUser(username);

            Thread.Sleep(5000);

            Assert.True(gitHubPage.IsUserLabelVisible(username));
        }
    }
}