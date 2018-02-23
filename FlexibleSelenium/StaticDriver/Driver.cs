using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace FlexibleSelenium.StaticDriver
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static int WaitMilliseconds { get; set; }
        public static string BaseUrl { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static void Initialize(string driver, int waitMilliseconds, string baseUrl, string username, string password)
        {
            driver = driver.ToLower();
            WaitMilliseconds = waitMilliseconds;
            BaseUrl = baseUrl;
            Username = username;
            Password = password;

            switch (driver)
            {
                case ("firefox"):
                    Instance = new FirefoxDriver();
                    break;

                case ("chrome"):
                    ChromeOptions options = new ChromeOptions();

                    options.AddArgument("test-type");
                    options.AddArgument("disable-extensions");
                    options.AddArgument("disable-infobars");
                    options.AddArgument("window-size=1600,900");

                    Instance = new ChromeDriver(options);
                    break;

                case ("edge"):
                    Instance = new EdgeDriver();
                    break;

                case ("remote"):
                    Instance = new RemoteWebDriver(DesiredCapabilities.HtmlUnitWithJavaScript());
                    break;

                default:
                    Instance = new ChromeDriver();
                    new System.ComponentModel.WarningException("Invalid driver type (a Chrome browser has been chosen instead).");
                    break;
            }
        }

        public static void Close()
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            Instance.Close();
        }

        public static void GoToUrl(string url)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            Instance.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElement(By by)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            return Instance.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            return Instance.FindElements(by);
        }


        //Not sure if these are at all useful, this seems to be the default behavior

        //public static void WaitForLoad(int waitMilliseconds)
        //{
        //    WebDriverWait wait = new WebDriverWait(Instance, new TimeSpan(0, 0, WaitMilliseconds));
        //    wait.Until(d => ((IJavaScriptExecutor)Instance).ExecuteScript("return document.readyState").Equals("complete"));
        //}

        //public static void WaitForLoad()
        //{
        //    WaitForLoad(WaitMilliseconds);
        //}
    }
}
