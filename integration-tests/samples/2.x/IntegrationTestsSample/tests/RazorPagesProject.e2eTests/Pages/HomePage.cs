using OpenQA.Selenium;


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

        private IWebElement FirstMessageOnList;

        private IWebElement MessagesPanel;

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
            FirstMessageOnList = _WebDriver.FindElement(By.XPath("//*[@id=\"messages\"]/div/div[2]/ul/li[1]"));

            return FirstMessageOnList.Text.Contains(message);
        }

        public void DeleteAllMessages()
        {
            DeleteAllButton = _WebDriver.FindElement(By.Id("deleteAllBtn"));

            DeleteAllButton.Click();
        }

        public bool IsMessagesListEmpty()
        {
            MessagesPanel = _WebDriver.FindElement(By.XPath("//*[@id=\"messages\"]/div/div[2]"));

            return MessagesPanel.Text.Contains("");
        }
    }
}