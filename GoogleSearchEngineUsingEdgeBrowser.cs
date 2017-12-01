using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Edge;

namespace Selenium3_7_VisualStudio2017
{
    [TestClass]
    public class GoogleSearchEngineUsingEdgeBrowser
    {
        // EdgeDriver full path: @"C:\SeleniumEdgeDriver\MicrosoftWebDriver.exe”
        string edgeDriverLocation = @"C:\SeleniumEdgeDriver";

        [TestMethod]
        public void Shoud_Search_Using_EdgeBrowser()
        {
            // Initialize the IE Driver
            using (var driver = new EdgeDriver(edgeDriverLocation))
            {
                // 1. Maximize the browser
                driver.Manage().Window.Maximize();

                // 2. Go to the "Google" homepage
                driver.Navigate().GoToUrl("http://www.google.com");

                // 3. Find the search textbox (by ID) on the homepage
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                var searchBox = driver.FindElementById("lst-ib");

                // 4. Enter the text (to search for) in the textbox
                searchBox.SendKeys("Automation using selenium 3.0 in C#");

                // 5. Find the search button (by Name) on the homepage
                var searchButton = driver.FindElementByName("btnK");

                // 6. Click "Submit" to start the search
                searchButton.Submit();

                // 7. Find the "Id" of the "Div" containing results stats, just before the results table.
                var searchResults = driver.FindElementById("resultStats");
            }
        }
    }
}
