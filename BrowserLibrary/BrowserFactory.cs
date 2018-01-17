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
            // We should also close any drivers hanging from last test
            CloseAllDriverProcessesForThisBrowser(browserName);
        }

        public static IWebDriver OpenBrowserInstanceFor(string browserName)
        {
            string driverLocation = @"C:\SeleniumTools\Browsers";
            switch (browserName.ToUpper())
            {
                case "FIREFOX":
                    {
                        _driver = new FirefoxDriver(driverLocation);
                    }
                    break;

                case "FIREFOX_HEADLESS":
                    {
                        FirefoxOptions options = new FirefoxOptions();
                        options.AddArguments("--headless");
                        _driver = new FirefoxDriver(driverLocation,options);

                    }
                    break;


                case "IE":
                case "IEXPLORE":
                    {
                        _driver = new InternetExplorerDriver(driverLocation);
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
                        _driver = new ChromeDriver(driverLocation,options);
                    }
                    break;

                case "EDGE":
                    {
                        _driver = new EdgeDriver(driverLocation);
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Invalid driver!!");
                        throw new DriverServiceNotFoundException ();
                    }
            }
            return _driver;
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

    }
}