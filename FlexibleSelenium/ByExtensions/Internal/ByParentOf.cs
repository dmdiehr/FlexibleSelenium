using FlexibleSelenium.IWebElementExtensions;
using FlexibleSelenium.PageElements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByParentOf : By
    {
        PageElement ChildElement;

        internal ByParentOf(By childBy)
        {
            ChildElement = new PageElement(childBy);
        }

        internal ByParentOf(PageElement childElement)
        {
            ChildElement = childElement;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ChildElement.FindElement(By.XPath(".."));
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ChildElement.FindElements(By.XPath(".."));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByParentOf([{0}])", ChildElement);
        }
    }
}