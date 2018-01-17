using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using BrowserLibrary;

namespace Pages
{
    public class GoogleHomePage
    {

        private IWebDriver _driver;
        private string _googleHomepage = "https://www.google.com";

        [FindsBy(How = How.Id, Using = "lst-ib")]
        public IWebElement searchTextbox;

        [FindsBy(How = How.Name, Using = "btnK")]
        public IWebElement googleSearchButton;

        public GoogleHomePage(string browserName)
        {
            // Initialize (and save) a driver instance for the selected browser
            this._driver = BrowserFactory.OpenBrowserInstanceFor(browserName);
            //this._driver = BrowserFactory.Drivers[browserName.ToUpper()];
            PageFactory.InitElements(this._driver, this);
        }

        public GoogleHomePage NavigateToGoogleSearchPage()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(this._googleHomepage);
            return this;
        }

        public GoogleHomePage EnterTextInSearchBox(string text)
        {
            searchTextbox.SendKeys(text);
            searchTextbox.SendKeys(Keys.Escape);
            return this;
        }

        public ResultsPage ClickGoogleSearchButton()
        {
            googleSearchButton.Click();
            return new ResultsPage(_driver);
        }
    }
}