using BrowserLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;


namespace Pages
{
    [TestClass]
    public class SearchingWithBingEngineToDelete
    {

        // timers
        //private Stopwatch _stopWatch;

        //string homePageGoogle = "http://www.google.com";
        string homePageBing = "http://www.bing.com";
        string stringToSearchFor = @"Selenium - Web Browser Automation";
        //string fullPath = @"C:\SeleniumTools\Browsers";


        //public TestContext TestContext { get; set; }
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }

        [TestMethod]
        [DataSource("System.Data.Odbc",
            "Dsn=Excel Files;" +
            "Driver={Microsoft Excel Driver (*.xls)};" +
            "dbq=|DataDirectory|\\Browsers.xlsx;" +
            "defaultdir=.;" +
            "driverid=790;" +
            "maxbuffersize=2048;" +
            "pagetimeout=5;" +
            "readonly=true", "BrowserName$", DataAccessMethod.Sequential)]

        public void Should_Search_Using_Bing_Search_Engine()
        {

            //TestContext.BeginTimer("mytest");
            //System.Threading.Thread.Sleep(5 * 1000);
            //TestContext.EndTimer("mytest");
            string browserName = string.Empty;
            try
            {
                // 1. Get the browser name from the data source
                browserName = TestContext.DataRow["Browsername"].ToString();
                //TestContext.WriteLine($"Browser name: {browserName}");
                //Console.WriteLine($"Using browser: {browserName}");

                // 2. Close all browser instances before starting the test
                BrowserFactory.CloseAllInstancesForBrowser(browserName);

                // 3. Initialize a driver instance for the selected browser
                IWebDriver driver = BrowserFactory.OpenBrowserInstanceFor(browserName);

                //FirefoxOptions options = new FirefoxOptions();
                //options.AddArguments("--headless");
                //_driver = new FirefoxDriver(options); 
                ////IWebDriver driver = new RemoteWebDriver(new Uri("http://192.168.1.8:4444/wd/hub"), DesiredCapabilities.Chrome());

                // 4. Maximize the browser window
                driver.Manage().Window.Maximize();

                // 5. Navigate to Bing home page
                driver.Navigate().GoToUrl(homePageBing);

                // 6. Find th search textbox on the page
                IWebElement searchBox = driver.FindElement(By.Id("sb_form_q"));

                // 7. Enter the text to search for
                searchBox.SendKeys(stringToSearchFor);

                // 8. Find the search button
                IWebElement searchButton = driver.FindElement(By.Id("sb_form_go"));

                // 7. Click the search button
                searchButton.Click();

                //// 8. Close the current window. If last window in browser, close the browser.
                //internetExplorerDriver.Close();

                // 9. "internetExplorerDriver.Close();" is not closing the windows, asexpected, at this point. Is it a bug? not sure...
                // So, let us close it by process id. Note: we should close only the windows that we opened during this test. (TBD)
                //BrowserFactory.CloseBrowsersUsedInThisTest(browserName);// (_startupIEProcessIds);
                //Console.WriteLine($"Test completed: {browserName}");
                BrowserFactory.CloseAllInstancesForBrowser(browserName);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {browserName} => {e.Message } ");
                Assert.Fail(e.Message);
            }
        }
    }
}
