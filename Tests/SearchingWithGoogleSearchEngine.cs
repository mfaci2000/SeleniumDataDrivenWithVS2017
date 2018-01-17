using BrowserLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Pages
{
    [TestClass]
    public class SearchingWithGoogleSearchEngine
    {
        private string stringToSearchFor = @"Test Automation using Selenium and C#";
        private string browserName = string.Empty;

        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("System.Data.Odbc",
            "Dsn=Excel Files;" +
            "Driver={Microsoft Excel Driver (*.xls)};" +
            "dbq=|DataDirectory|\\Browsers.xlsx;" +
            "defaultdir=.;" +
            "driverid=790;" +
            "maxbuffersize=2048;" +
            "pagetimeout=5;" +
            "readonly=true", "Browser$", DataAccessMethod.Sequential)]

        public void Should_Search_Using_Bing_Search_Engine()
        {
            // 1. Get the browser name from the data source
            browserName = TestContext.DataRow["BrowserName"].ToString();

            // 2. Close all browser instances (for this type) before starting the test
            BrowserFactory.CloseAllInstancesForBrowser(browserName);

            // Start the test
            GoogleHomePage googleHomePage = new GoogleHomePage(browserName);
            googleHomePage
                .NavigateToGoogleSearchPage()
                .EnterTextInSearchBox(stringToSearchFor)
                .ClickGoogleSearchButton()
                .VerifyThatResultsPageIsDisplayed()
                .CloseBrowser(browserName)
                ;
        }
    }
}
