using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;

namespace FlexibleSelenium.Tests
{
    public static class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void Initialize(string driver = "chrome")
        {
            switch (driver.ToLower())
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

                default:
                    Instance = new FirefoxDriver();
                    new System.ComponentModel.WarningException("Invalid driver type (a Firefox browser has been chosen instead).");
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

        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Instance.FindElements(by);
        }

        public static IWebElement FindElement(By by)
        {
            return Instance.FindElement(by);
        }
    }
}
