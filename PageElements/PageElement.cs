using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.PageElements
{
    public class PageElement : IWebElement
    {
        protected ISearchContext Context { get; }
        private By TargetBy;
        protected IWebDriver ThisDriver { get; }
        public int WaitMilliseconds { get; set; }
        public IWebElement BaseElement => GetBaseElement(WaitMilliseconds);

        //Properties required to implement IWebElement
        public string TagName => BaseElement.TagName;
        public string Text => BaseElement.Text;
        public bool Enabled => BaseElement.Enabled;
        public bool Selected => BaseElement.Selected;
        public Point Location => BaseElement.Location;
        public Size Size => BaseElement.Size;
        public bool Displayed => BaseElement.Displayed;

        //Ctors
        public PageElement(IWebDriver driver, By targetBy, int waitMilliseconds)
        {
            ThisDriver = driver;
            TargetBy = targetBy;
            Context = driver;
            WaitMilliseconds = waitMilliseconds;
        }

        public PageElement(IWebDriver driver, By contextBy, By targetBy, int waitMilliseconds)
        {
            ThisDriver = driver;
            TargetBy = targetBy;
            Context = new PageElement(driver, contextBy, waitMilliseconds);
            WaitMilliseconds = waitMilliseconds;
        }

        public PageElement(PageElement contextElement, By targetBy, int waitMilliseconds)
        {
            ThisDriver = contextElement.ThisDriver;
            TargetBy = targetBy;
            Context = contextElement;
            WaitMilliseconds = waitMilliseconds;
        }

        /// <summary>
        /// Only use this constructor overload if you are also using FlexibleSelenium.StaticDriver to manage your driver instance
        /// </summary>
        public PageElement(By targetBy)
        {
            ThisDriver = Driver.Instance;
            Context = Driver.Instance;
            TargetBy = targetBy;
            WaitMilliseconds = Driver.WaitMilliseconds;
        }

        /// <summary>
        /// Only use this constructor overload if you are also using FlexibleSelenium.StaticDriver to manage your driver instance
        /// </summary>
        public PageElement(By contextBy, By targetBy)
        {
            ThisDriver = Driver.Instance;
            Context = new PageElement(contextBy);
            TargetBy = targetBy;
            WaitMilliseconds = Driver.WaitMilliseconds;
        }

        /// <summary>
        /// Only use this constructor overload if you are also using FlexibleSelenium.StaticDriver to manage your driver instance
        /// </summary>
        public PageElement(PageElement contextElement, By targetBy)
        {
            ThisDriver = Driver.Instance;
            Context = contextElement;
            TargetBy = targetBy;
            WaitMilliseconds = Driver.WaitMilliseconds;
        }

        protected IWebElement GetBaseElement(int waitMilliseconds)
        {
            var e = new Exception();
            bool exceptionThrown = false;
            var startTime = DateTime.Now;
            var currentTime = startTime;

            while (currentTime.Subtract(startTime).TotalMilliseconds <= waitMilliseconds)
            {
                exceptionThrown = false;
                try
                {
                    return Context.FindElement(TargetBy);
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException || ex is StaleElementReferenceException)
                {
                    e = ex;
                    exceptionThrown = true;
                }

                currentTime = DateTime.Now;
            }

            if (exceptionThrown)
            {
                var newMessage = e.Message + " Exception not resolved within allowed time of: " + waitMilliseconds + " milliseconds";
                throw new Exception(newMessage, e);
            }
            else
                throw new ApplicationException("An unexpected exception has occured in the GetBaseElement method");
        }

        /// <summary>
        /// Returns false if getting the BaseElement throws an exception. Otherwise it returns the value of the 
        /// BaseElement.Displayed. The browser will scroll to the element because EdgeDriver will evaluate Displayed as false 
        /// if the element is not within view.
        /// </summary>
        public virtual bool IsPresent(int waitMilliseconds)
        {
            try
            {
                var baseElement = GetBaseElement(waitMilliseconds);
                ScrollTo();
                return baseElement.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException || ex is NullReferenceException)
            {
                return false;
            }
        }

        public virtual bool IsPresent()
        {
            return IsPresent(WaitMilliseconds);
        }

        /// <summary>
        /// Shortcut to create a MoveToElement action that targets the BaseElement of this Element.
        /// </summary>
        /// <param name="catchInvalidOperationException">Unfortunately, Edge behaves differently than Chrome and Firefox in that it does not let the MoveToElement fail quietly.
        /// So this method catches the exception that EdgeDriver throws. Set this param to false if you do not want the exception caught.</param>
        public void ScrollTo(bool catchInvalidOperationException = true)
        {
            try
            {
                Actions actions = new Actions(ThisDriver);
                actions.MoveToElement(BaseElement);
                actions.Perform();
            }
            catch (InvalidOperationException ex)
            {
                if (!catchInvalidOperationException)
                    throw ex;
            }
        }

        //Methods required to implement IWebElement
        public IWebElement FindElement(By by)
        {
            return BaseElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return BaseElement.FindElements(by);
        }

        public void Click() //Wraps BaseElement.Click around an ElementNotVisibleException catch for the length of WaitMilliseconds
        {
            var e = new Exception();
            bool exceptionThrown = false;
            var startTime = DateTime.Now;
            var currentTime = startTime;

            while (currentTime.Subtract(startTime).TotalMilliseconds <= WaitMilliseconds)
            {
                exceptionThrown = false;
                try
                {
                    BaseElement.Click();
                    return;
                }
                catch (ElementNotVisibleException ex)
                {
                    exceptionThrown = true;
                    e = ex;
                }

                currentTime = DateTime.Now;
            }

            if (exceptionThrown)
            {
                var newMessage = e.Message + " Exception not resolved within allowed time of: " + WaitMilliseconds + " milliseconds";
                throw new Exception(newMessage, e);
            }
            else
                throw new ApplicationException("An unexpected exception has occured in BaseElement.Click");
        }

        public void Clear()
        {
            BaseElement.Clear();
        }

        public void SendKeys(string text) //Wraps BaseElement.Senkeys around an ElementNotVisibleException catch for the length of WaitMilliseconds
        {
            var e = new Exception();
            bool exceptionThrown = false;
            var startTime = DateTime.Now;
            var currentTime = startTime;

            while (currentTime.Subtract(startTime).TotalMilliseconds <= WaitMilliseconds)
            {
                exceptionThrown = false;
                try
                {
                    BaseElement.SendKeys(text);
                    return;
                }
                catch (ElementNotVisibleException ex)
                {
                    exceptionThrown = true;
                    e = ex;
                }

                currentTime = DateTime.Now;
            }

            if (exceptionThrown)
            {
                var newMessage = e.Message + " Exception not resolved within allowed time of: " + WaitMilliseconds + " milliseconds";
                throw new Exception(newMessage, e);
            }
            else
                throw new ApplicationException("An unexpected exception has occured in BaseElement.Sendkeys");
        }

        public void Submit()
        {
            BaseElement.Submit();
        }

        public string GetAttribute(string attributeName)
        {
            return BaseElement.GetAttribute(attributeName);
        }

        public string GetProperty(string propertyName)
        {
            return BaseElement.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return BaseElement.GetCssValue(propertyName);
        }
    }
}
