using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkBrowserLibrary;
using Pages;

// https://www.swtestacademy.com/page-object-model-c/
namespace Tests
{
    [TestClass]
    public class Searching
    {
        //private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            //driver = new ChromeDriver();
            //driver.Manage().Window.Maximize();
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
            "readonly=true", "BrowserNames$", DataAccessMethod.Sequential)]

        public void ShouldDisplayResultsWhenEnteringNonEmptySearchTerm()// SearchTextFromAboutPage()
        {

            // 1. Get the browser name from the data source
            string selectedBrowser = TestContext.DataRow["Browsername"].ToString();

            GoogleHomePage googleHomePage = new GoogleHomePage(selectedBrowser);
            googleHomePage.NavigateToGoogleSearchPage();

            //string browserName = string.Empty;
            //try
            //{


            //    // 2. Close all browser instances before starting the test
            //    BrowserFactory.CloseAllInstancesForBrowser(selectedBrowser);

            //    // 3. Initialize a driver instance for the selected browser
            //    IWebDriver driver = BrowserFactory.OpenInstanceFor(selectedBrowser);
            //}

            //    HomePage home = new HomePage(driver);
            //home.goToPage();
            //AboutPage about = home.goToAboutPage();
            //ResultPage result = about.search("selenium c#");
            //result.clickOnFirstArticle();
        }

        [TestCleanup]
        public void TearDown()
        {
            //driver.Close();
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //}

        //public TestContext TestContext { get; set; }
        private TestContext context;

        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }
    }
}

//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using POMExample.PageObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
 
//namespace POMExample
//{
//    public class TestClass
//    {
//        private IWebDriver driver;

//        [SetUp]
//        public void SetUp()
//        {
//            driver = new ChromeDriver();
//            driver.Manage().Window.Maximize();
//        }

//        [Test]
//        public void SearchTextFromAboutPage()
//        {
//            HomePage home = new HomePage(driver);
//            home.goToPage();
//            AboutPage about = home.goToAboutPage();
//            ResultPage result = about.search("selenium c#");
//            result.clickOnFirstArticle();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            driver.Close();
//        }
//    }
//}
