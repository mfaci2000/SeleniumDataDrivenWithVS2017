using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class GoogleHomePage
    {

        private IWebDriver driver;

        public GoogleHomePage(string browserName)
        {
            
            //BrowserFactory.CloseAllInstancesForBrowser(browserName);

            // 3. Initialize a driver instance for the selected browser
            IWebDriver driver = BrowserLibrary.BrowserFactory.OpenInstanceFor(browserName);


            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //public GoogleHomePage(IWebDriver driver)
        //{
        //    this.driver = driver;
        //    PageFactory.InitElements(driver, this);
        //}

        //[FindsBy(How = How.CssSelector, Using = ".fusion-main-menu a[href*='about']")]
        //private IWebElement about;

        //[FindsBy(How = How.ClassName, Using = "fusion-main-menu-icon")]
        //private IWebElement searchIcon;

        //public void goToPage()
        //{
        //    driver.Navigate().GoToUrl("https://www.google.com");
        //}

        [FindsBy(How = How.Id, Using = "sb_form_q")]
        private IWebElement searchTextBox;

        public void NavigateToGoogleSearchPage()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        //public AboutPage goToAboutPage()
        //{
        //    about.Click();
        //    return new AboutPage(driver);
        //}

    }
}