using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using FlexibleSelenium.PageElements;

namespace FlexibleSelenium.PageElements.Extensions
{
    public class IconLinkElement : PageElement
    {
        public IconLinkElement(IWebDriver driver, By targetBy, int waitMilliseconds) : base(driver, targetBy, waitMilliseconds) { }
        public IconLinkElement(IWebDriver driver, By contextBy, By targetBy, int waitMilliseconds) : base(driver, contextBy, targetBy, waitMilliseconds) { }
        public IconLinkElement(PageElement contextElement, By targetBy, int waitMilliseconds) : base(contextElement, targetBy, waitMilliseconds){ }
        public IconLinkElement(By contextBy, By targetBy) : base(contextBy, targetBy) { }
        public IconLinkElement(By targetBy) : base(targetBy) { }
        public IconLinkElement(PageElement contextElement, By targetBy) : base(contextElement, targetBy) { }


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
