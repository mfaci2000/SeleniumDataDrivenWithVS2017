using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserLibrary;

namespace Pages
{
    public class ResultsPage
    {
        private IWebDriver _driver;

        public ResultsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(this._driver, this);
        }

        [FindsBy(How = How.Id, Using = "resultStats")]
        public IWebElement resultStatsLabel;

        public ResultsPage VerifyThatResultsPageIsDisplayed()
        {
            // Sometimes, the Edge browser is very fast. Let's wait abit.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.IsTrue(resultStatsLabel.Text.Length > 0);
            return this;
        }

        public void CloseBrowser(string browserName)
        {
            BrowserFactory.CloseAllInstancesForBrowser(browserName);
        }
    }
}