using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBrowserLibrary
{
    public static class BrowserType
    {
        public static Dictionary<string, string> Name = new Dictionary<string, string>()
        {
            {"EDGE", "MicrosoftEdge"},
            {"CHROME", "chrome" },
            {"CHROME_HEADLESS", "chrome" },
            {"FIREFOX", "firefox" },
            {"FIREFOX_HEADLESS", "firefox" },
            {"IE", "iexplore" },
            {"PHANTOMJS_HEADLESS", "phantomJS" },
        };

        public enum SeleniumDriver
        {
            IEDriverServer,
            geckodriver,
            chromedriver,
            edge,
            MicrosoftWebDriver,
            phantomJS
        }
    }
}