using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using FlexibleSelenium.PageElements;

namespace FlexibleSelenium.PageElements.Extensions
{
    public class IconLinkElement : PageElement
    {
        public IconLinkElement(IWebDriver driver, By targetBy, int waitMilliseconds = 2000) : base(driver, targetBy, waitMilliseconds) { }

        public IconLinkElement(IWebDriver driver, By contextBy, By targetBy, int waitMilliseconds = 2000) : base(driver, contextBy, targetBy, waitMilliseconds) { }

        /// <summary>
        /// Checks that element's Size is not empty and the visibility value of its CSS.
        /// </summary>
        /// <param name="waitMilliseconds">The length of time in milliseconds to continue to look for the element before returning false.</param>
        /// <returns>Returns a bool based on whether the element exists and is visible</returns>
        public override bool IsPresent(int waitMilliseconds)
        {
            try
            {
                var baseElement = GetBaseElement(waitMilliseconds);
                ScrollTo();

                return baseElement.GetCssValue("visibility") == "visible" && !baseElement.Size.IsEmpty;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException || ex is NullReferenceException)
            {
                return false;
            }
        }
    }
}
