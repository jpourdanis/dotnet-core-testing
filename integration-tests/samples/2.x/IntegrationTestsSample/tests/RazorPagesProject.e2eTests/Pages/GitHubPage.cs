using OpenQA.Selenium;

namespace RazorPagesProject.e2eTests.Pages
{
    public class GitHubPage
    {
        private IWebDriver _WebDriver;

        public GitHubPage(IWebDriver webDriver)
        {
            _WebDriver = webDriver;

            _WebDriver.Navigate().GoToUrl("https://localhost:5001/GithubProfile");

        }

        // Locators

        private IWebElement UserNameTextBox;

        private IWebElement ShowProfileButton;

        private IWebElement UserNameLabel;

        // Actions

        public void SearchForUser(string username)
        {
            UserNameTextBox = _WebDriver.FindElement(By.Id("Input_UserName"));

            UserNameTextBox.SendKeys(username);

            ShowProfileButton = _WebDriver.FindElement(By.XPath("//button[@type='submit']"));

            ShowProfileButton.Click();
        }

        public bool IsUserLabelVisible(string username)
        {
            UserNameLabel = _WebDriver.FindElement(By.Id("user-login"));

            return UserNameLabel.Text.Contains(username);
        }


    }
}
