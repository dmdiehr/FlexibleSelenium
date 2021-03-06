﻿using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using FlexibleSelenium.StaticDriver;
using FlexibleSelenium.ByExtensions;
using FlexibleSelenium.IWebElementExtensions;

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
                if(e is NoSuchElementException)
                    throw new NoSuchElementException(newMessage, e);
                if (e is WebDriverException)
                    throw new WebDriverException(newMessage, e);
                if (e is StaleElementReferenceException)
                    throw new StaleElementReferenceException(newMessage, e);
                else
                    throw new ApplicationException("An unexpected exception has occured in the GetBaseElement method");
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

        /// <summary>
        /// Returns false if getting the BaseElement throws an exception. Otherwise it returns the value of the 
        /// BaseElement.Displayed. The browser will scroll to the element because EdgeDriver will evaluate Displayed as false 
        /// if the element is not within view.
        /// </summary>
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
        #region

        /// <summary>
        /// Finds the first IWebElement using the given mechanism.
        /// <see cref="ISearchContext.FindElement(By)"/>
        /// </summary>
        public IWebElement FindElement(By by)
        {
            return BaseElement.FindElement(by);
        }

        /// <summary>
        /// Finds all IWebElement within the context using the given mechanism.
        /// <see cref="ISearchContext.FindElements(By)"/>
        /// </summary>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return BaseElement.FindElements(by);
        }

        /// <summary>
        /// Wraps BaseElement.Click within an ElementNotVisibleException and InvalidOperationException catch for the length of WaitMilliseconds
        /// </summary>
        public void Click()
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
                catch (Exception ex) when (ex is ElementNotVisibleException || ex is InvalidOperationException)
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

        /// <summary>
        /// Wraps BaseElement.Clear, will catch a StaleElementReferenceException and try again one time.
        /// </summary>
        public void Clear()
        {
            try
            {
                BaseElement.Clear();
            }
            catch (StaleElementReferenceException)
            {

                BaseElement.Clear();
            }

        }

        /// <summary>
        //// Wraps BaseElement.SendKeys within an ElementNotVisibleException and InvalidOperationException catch for the length of WaitMilliseconds
        /// </summary>
        public void SendKeys(string text) //
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
                catch (Exception ex) when (ex is ElementNotVisibleException || ex is InvalidOperationException)
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

        /// <summary>
        /// Wraps BaseElement.Submit, will catch a StaleElementReferenceException and try again one time.
        /// </summary>
        public void Submit()
        {
            try
            {
                BaseElement.Submit();
            }
            catch (StaleElementReferenceException)
            {
                BaseElement.Submit();
            }
            
        }

        /// <summary>
        /// Wraps BaseElement.GetAttribute, will catch a StaleElementReferenceException and try again one time.
        /// </summary>
        public string GetAttribute(string attributeName)
        {
            try
            {
                return BaseElement.GetAttribute(attributeName);
            }
            catch (StaleElementReferenceException)
            {
                return BaseElement.GetAttribute(attributeName);
            }
            
        }

        /// <summary>
        /// Wraps BaseElement.GetProperty, will catch a StaleElementReferenceException and try again one time.
        /// </summary>
        public string GetProperty(string propertyName)
        {
            try
            {
                return BaseElement.GetProperty(propertyName);
            }
            catch (StaleElementReferenceException)
            {
                return BaseElement.GetProperty(propertyName); //one more try
            }
            
        }

        /// <summary>
        /// Wraps BaseElement.GetCssValue, will catch a StaleElementReferenceException and try again one time.
        /// </summary>
        public string GetCssValue(string propertyName)
        {
            try
            {
                return BaseElement.GetCssValue(propertyName);
            }
            catch (StaleElementReferenceException)
            {
                return BaseElement.GetCssValue(propertyName); ;
            }            
        }
        #endregion

        /// <summary>
        /// Locates the element based on the linkText argument and performs the Click method on that element
        /// </summary>
        /// <remarks>Uses ByExtension.TextAndTag to avoid inconsistent results from By.LinkText</remarks>
        public void ClickLink(string linkText)
        {
            this.FindElement(ByExtension.TextAndTag(linkText, "a")).Click();
        }

        /// <summary>
        /// Returns a string representation of an XPath query that can be used to find this.BaseElement
        /// </summary>
        public string GetXPath()
        {
            try
            {
                return BaseElement.GetXPath();
            }
            catch (StaleElementReferenceException)
            {
                return BaseElement.GetXPath(); //one more try
            }
        }

        /// <summary>
        /// Evaluates whether the childElement is both IsPresent and within the scope of this PageElement's BaseElement.
        /// </summary>
         public bool Contains(PageElement childElement)
        {
            if (this.IsPresent(WaitMilliseconds))
            {
                if (childElement.IsPresent(WaitMilliseconds))
                {
                    var allDesendantElements = this.BaseElement.GetDescendants();

                    return allDesendantElements.Contains(childElement.BaseElement);
                }
                else
                    return false;
            }
            else
                throw new NoSuchElementException("The proposed parent element is not present, and thus cannot contain any children elements");

        }

        public bool Contains(By childBy)
        {
            var childElement = new PageElement(childBy);
            return Contains(childElement);
        }
    }
}
