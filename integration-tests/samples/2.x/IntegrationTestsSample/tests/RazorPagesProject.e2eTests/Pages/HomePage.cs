using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace RazorPagesProject.e2eTests.Pages
{
    public class HomePage
    {
        private IWebDriver _WebDriver;

        public HomePage(IWebDriver webDriver )
        {
            _WebDriver = webDriver;
        }

        // Locators

        private IWebElement MessageTextField;

        private IWebElement AddMessageButton;

        private IWebElement DeleteAllButton;

        private IWebElement FirstMessage;

        //Actions

        public void AddMessage(string message)
        {
            MessageTextField = _WebDriver.FindElement(By.Id("Message_Text"));

            MessageTextField.SendKeys(message);

            AddMessageButton = _WebDriver.FindElement(By.Id("addMessageBtn"));

            AddMessageButton.Click();
        }

        public bool IsMessageAppearToList(string message)
        {
            FirstMessage = _WebDriver.FindElement(By.XPath("//*[@id=\"messages\"]/div/div[2]/ul/li[1]"));

            return FirstMessage.Text.Contains(message);
        }

        public void DeleteAllMessages()
        {
            DeleteAllButton = _WebDriver.FindElement(By.Id("deleteAllBtn"));

            DeleteAllButton.Click();
        }
    }
}