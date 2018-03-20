using FlexibleSelenium.PageElements;
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

        /// <summary>
        /// Close the current window, quitting the browser if it is the last window currently open.
        /// </summary>
        public static void Close()
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            Instance.Close();
        }

        /// <summary>
        /// Load a new web page in the current browser window.
        /// </summary>
        public static void GoToUrl(string url)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            Instance.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Finds the first IWebElement using the given mechanism.
        /// <see cref="ISearchContext.FindElement(By)"/>
        /// </summary>
        public static IWebElement FindElement(By by)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            return Instance.FindElement(by);
        }

        /// <summary>
        /// Finds all IWebElement within the context using the given mechanism.
        /// <see cref="ISearchContext.FindElements(By)"/>
        /// </summary>
        public static ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            if (Instance == null)
            {
                throw new ApplicationException("FlexibileSelenium.StaticDriver.Driver.Instance is null. Please call Driver.Initilalize to create a usable instance");
            }
            return Instance.FindElements(by);
        }

        /// <summary>
        /// Waits until the given PageElement is present before proceeding.
        /// </summary>
        /// <exception cref="StaticDriver.AwaitElementException">Exception that is thrown if the element is not present within the given wait time</exception>
        /// <param name="waitMilliseconds">Length of time to wait in milliseconds before throwin an exception</param>
        /// <param name="exceptionMessage">Exception message that will accompany the AwaitElementException if it is thrown.</param>
        public static void AwaitElement(PageElement element, int waitMilliseconds, string exceptionMessage = "The provided element was not present within the allotted time.")
        {
            var currentTime = DateTime.Now;
            var startTime = currentTime;

            while (currentTime.Subtract(startTime).TotalMilliseconds <= waitMilliseconds)
            {
                if (element.IsPresent(waitMilliseconds))
                    return;

                currentTime = DateTime.Now;
            }
            throw new AwaitElementException(exceptionMessage);
        }

        /// <summary>
        /// Waits until the given PageElement is present before proceeding.
        /// </summary>
        /// <exception cref="StaticDriver.AwaitElementException">Exception that is thrown if the element is not present within the wait time currently set in the StaticDriver.WaitMilliseconds property</exception>
        /// <param name="exceptionMessage">Exception message that will accompany the AwaitElementException if it is thrown.</param>
        public static void AwaitElement(PageElement element, string exceptionMessage = "The provided element was not present within the allotted time.")
        {
            AwaitElement(element, WaitMilliseconds, exceptionMessage);
        }

        /// <summary>
        /// Waits until the PageElement instantiated with the provided By is present before proceeding.
        /// </summary>
        /// <exception cref="StaticDriver.AwaitElementException">Exception that is thrown if the element is not present within the given wait time</exception>
        /// <param name="waitMilliseconds">Length of time to wait in milliseconds before throwin an exception</param>
        /// <param name="exceptionMessage">Exception message that will accompany the AwaitElementException if it is thrown.</param>
        public static void AwaitElement(By by, int waitMilliseconds, string exceptionMessage = "The provided element was not present within the allotted time.")
        {
            var element = new PageElement(by);
            AwaitElement(element, waitMilliseconds, exceptionMessage);

        }

        /// <summary>
        /// Waits until the PageElement instantiated with the provided By is present before proceeding.
        /// </summary>
        /// <exception cref="StaticDriver.AwaitElementException">Exception that is thrown if the element is not present within the wait time currently set in the StaticDriver.WaitMilliseconds property</exception>
        /// <param name="exceptionMessage">Exception message that will accompany the AwaitElementException if it is thrown.</param>
        public static void AwaitElement(By by, string exceptionMessage = "The provided element was not present within the allotted time.")
        {
            AwaitElement(by, WaitMilliseconds, exceptionMessage);
        }
    }
}
