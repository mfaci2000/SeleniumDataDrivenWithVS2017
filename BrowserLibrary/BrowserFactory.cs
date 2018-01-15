using FrameworkBrowserLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BrowserLibrary
{
    public static class BrowserFactory
    {
        // Internet explorer specifics
        private static int[] _existingBrowsersIds;

        //private static string fullPath = @"C:\SeleniumTools\Browsers";
        public static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver _driver;



        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    throw new NullReferenceException("Browser instance not initialized!");
                return _driver;
            }
            private set
            {
                _driver = value;
            }
        }

        public static void KillProcessesById()
        {
            IEnumerable<int> pidsBefore = Process.GetProcessesByName("firefox").Select(p => p.Id);

            FirefoxDriver driver = new FirefoxDriver();
            IEnumerable<int> pidsAfter = Process.GetProcessesByName("firefox").Select(p => p.Id);

            IEnumerable<int> newFirefoxPids = pidsAfter.Except(pidsBefore);

            // do some stuff with PID, if you want to kill them, do the following
            foreach (int pid in newFirefoxPids)
            {
                Process.GetProcessById(pid).Kill();
            }
        }

        public static IWebDriver OpenInstanceFor(string browserName)
        {

            // Before starting a new browser, we keep track of existing instances (for the corresponding browser) that are open prior to starting the test.
            Console.WriteLine($"Browser name: {browserName} ");
            string driverLocation = @"C:\SeleniumTools\Browsers";
            switch (browserName.ToUpper())
            {
                case "FIREFOX":
                    {
                        _existingBrowsersIds = BrowserFactory.GetListOfIdsForProcessesOfType(BrowserType.SeleniumDriver.geckodriver.ToString());// "geckodriver"
                        _driver = new FirefoxDriver(driverLocation);
                    }
                    break;

                case "FIREFOX_HEADLESS":
                    {
                        FirefoxOptions options = new FirefoxOptions();
                        options.AddArguments("--headless");
                        _driver = new FirefoxDriver(options);

                    }
                    break;


                case "IE":
                case "IEXPLORE":
                    {
                        // NOTE: you may have to, manually,  set the security zones for the internert explorer on your machine. 
                        // follow this link. The link says "java", but it is just an IE setting, not java specific.
                        // https://stackoverflow.com/questions/14952348/not-able-to-launch-ie-browser-using-selenium2-webdriver-with-java
                        //

                        _existingBrowsersIds = BrowserFactory.GetListOfIdsForProcessesOfType("iexplore");
                        _driver = new InternetExplorerDriver(driverLocation);
                        //Drivers.Add("IE", Driver);
                    }
                    break;

                case "CHROME":
                    {
                        _driver = new ChromeDriver(driverLocation);
                    }
                    break;

                case "CHROME_HEADLESS":
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--headless");
                        _driver = new ChromeDriver(options);
                    }
                    break;

                case "EDGE":
                    {
                        //string edgeDriverLocation = @"C:\SeleniumEdgeDriver";
                        _driver = new EdgeDriver(driverLocation);// @"C:\PathTo\CHDriverServer");
                        //Drivers.Add("EDGE", Driver);
                    }
                    break;

                case "PHANTOMJS_HEADLESS":
                    {
                        //string edgeDriverLocation = @"C:\SeleniumTools\Browsers";// @"C:\SeleniumEdgeDriver";
                        _driver =  new PhantomJSDriver(driverLocation);// @"C:\PathTo\CHDriverServer");
                        //Drivers.Add("PHANTOMJS", Driver);
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Invalid driver!!");
                        throw new DriverServiceNotFoundException ();
                    }
            }
            //Drivers.Add(browserName.ToUpper(), Driver);
            return _driver;// BrowserFactory.Drivers[browserName.ToUpper()];
        }

        //public static void OpenInstancesOfBrowserUsedForTest(TestContext testContext)
        //{
        //    string browserName = testContext.DataRow["Browsername"].ToString();
        //    CloseAllDriverProcessesForThisBrowser(browserName);
        //}

        //public static void OpenBrowser(TestContext testContext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void OpenBrowser(TestContext testContext)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void OpenBrowser(TestContext testContext)
        //{
        //    throw new NotImplementedException();
        //}

        public static void CloseAllInstancesForBrowser(string browserName)
        {
            var existingBrowserProcesses = Process.GetProcessesByName(BrowserType.Name[browserName.ToUpper()]);
            foreach (var processId in existingBrowserProcesses)
            {
                if (!processId.HasExited)
                {
                    processId.Kill();
                    processId.WaitForExit();
                }
            }
            // We should also close any drivers hanging fromlast session.
            CloseAllDriverProcessesForThisBrowser(browserName);
        }

        public static void CloseAllDriverProcessesForThisBrowser(string browserName)
        {
            // Find the running processes for the given browser type, and terminate it.
            switch (browserName.ToUpper())
            {
                case "FIREFOX":
                case "FIREFOX_HEADLESS":
                    {
                        KillRunningProcesses(BrowserType.SeleniumDriver.geckodriver.ToString());
                    }
                    break;

                case "IE":
                case "IEXPLORE":
                    {
                        KillRunningProcesses(BrowserType.SeleniumDriver.IEDriverServer.ToString());
                    }
                    break;

                case "CHROME":
                case "CHROME_HEADLESS":
                    {
                        KillRunningProcesses(BrowserType.SeleniumDriver.chromedriver.ToString());
                    }
                    break;

                case "EDGE":
                    {
                        //KillRunningProcesses(BrowserType.SeleniumDriver.edge.ToString());
                        KillRunningProcesses(BrowserType.SeleniumDriver.MicrosoftWebDriver.ToString());
                        // MicrosoftWebDriver
                    }
                    break;

                case "PHANTOMJS_HEADLESS":
                    {
                        KillRunningProcesses(BrowserType.SeleniumDriver.phantomJS.ToString());
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Invalid driver!!");
                        throw new DriverServiceNotFoundException();
                    }
            }
        }

        private static void KillRunningProcesses(string driverType)
        {
            Process[] runningProcessesForDriverType = Process.GetProcessesByName(driverType);
            foreach (var processId in runningProcessesForDriverType)
            {
                processId.Kill();
                processId.WaitForExit();
            }
            
        }

        public static int[] GetListOfIdsForProcessesOfType(string processName)
        {
            return Process.GetProcessesByName(processName) // "iexplore" // geckodriver.exe
                              .Select(x => x.Id).ToArray();
        }

        //private static int[] GetListOfIdsForChromeProcesses()
        //{
        //    return Process.GetProcessesByName("chrome")
        //                      .Select(x => x.Id).ToArray();
        //}

        //private static int[] GetListOfIdsForFirefoxProcesses()
        //{
        //    return Process.GetProcessesByName("firefox")
        //                      .Select(x => x.Id).ToArray();
        //}

        private static void CloseAllBrowserWidowsExcept(int[] excludedBroserIds, string browserName)
        {
            var existingBrowserProcesses = Process.GetProcessesByName(browserName);// "iexplore");
            foreach (var processId in existingBrowserProcesses.Where(x => !excludedBroserIds.Contains(x.Id)))
            {
                processId.Kill();
                processId.WaitForExit();
            }
        }

        public static void CloseBrowsersUsedInThisTest(string browserName)
        {
            CloseAllBrowserWidowsExcept(_existingBrowsersIds, browserName);
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        //public static void CloseAllDrivers()
        //{
        //    foreach (var key in Drivers.Keys)
        //    {
        //        Drivers[key].Close();
        //        Drivers[key].Quit();
        //    }
        //}
    }
}