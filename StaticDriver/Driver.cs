using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
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
            Instance.Close();
        }

        public static void GoToUrl(string url)
        {
            Instance.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElement(By by)
        {
            return Instance.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
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
