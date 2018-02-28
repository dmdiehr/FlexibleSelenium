using FlexibleSelenium.IWebElementExtensions;
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
        By ChildBy;

        public ByParentOf(By childBy)
        {
            ChildBy = childBy;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var childElement = context.FindElement(ChildBy);
            return childElement.FindElement(By.XPath(".."));
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var childElement = context.FindElement(ChildBy);
            return childElement.FindElements(By.XPath(".."));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByParentOf([{0}])", ChildBy);
        }
    }
}