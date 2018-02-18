using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FlexibleSelenium.PageElements
{
    public class PageElement : IWebElement
    {
        private By ContextBy;
        private By TargetBy;
        private IWebDriver Driver;
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
        public PageElement(IWebDriver driver, By targetBy, int waitMilliseconds = 2000)
        {
            Driver = driver;
            TargetBy = targetBy;
            ContextBy = null;
        }

        public PageElement(IWebDriver driver, By contextBy, By targetBy, int waitMilliseconds = 2000)
        {
            Driver = driver;
            TargetBy = targetBy;
            ContextBy = contextBy;
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
                    if (ContextBy != null)
                    {
                        return Driver.FindElement(ContextBy).FindElement(TargetBy);
                    }
                    else
                    {
                        return Driver.FindElement(TargetBy);
                    }
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverException || ex is StaleElementReferenceException)
                {
                    e = ex;
                    exceptionThrown = true;
                }

                currentTime = DateTime.Now;
            }

            if (exceptionThrown)
                throw e;
            else
                throw new ApplicationException("An unexpected exception has occured in the GetBaseElement method");
        }


        /// <summary>
        /// Returns false if getting the BaseElement throws an exception. Otherwise it returns the value of the 
        /// BaseElement.Displayed except if the BaseElement is an anchor tag, in which case it will return 
        /// the Displayed value of the anchor tags parent (this is so links that are hidden and turned into 
        /// buttons, etc. will evaluate to true.The ScrollTo() is because EdgeDriver will evaluate Displayed as false 
        /// if the element is not within view.
        /// </summary>
        public virtual bool IsPresent(int waitMilliseconds)
        {
            try
            {
                var baseElement = GetBaseElement(waitMilliseconds);
                ScrollTo();

                //TODO - find valid way to determine if a-tag elements can be interacted with, i.e. find a way to determine
                //when an element's Displayed value is false but it has an icon or something else that is clickable.
                if (baseElement.TagName == "a")
                    return baseElement.FindElement(By.XPath("..")).Displayed;
                else
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
                Actions actions = new Actions(Driver);
                actions.MoveToElement(BaseElement);
                actions.Perform();
            }
            catch (InvalidOperationException ex)
            {
                if (!catchInvalidOperationException)
                    throw ex;
            }
        }

        //Methods required to implement ISearchContext
        public IWebElement FindElement(By by)
        {
            return BaseElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return BaseElement.FindElements(by);
        }

        public void Click()
        {
            BaseElement.Click();
        }

        public void Clear()
        {
            BaseElement.Clear();
        }

        public void SendKeys(string text)
        {
            BaseElement.SendKeys(text);
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
