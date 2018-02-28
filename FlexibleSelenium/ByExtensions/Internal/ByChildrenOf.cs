using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByChildrenOf : By
    {
        By ParentBy;

        public ByChildrenOf(By parentBy)
        {
            ParentBy = parentBy;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var childElement = context.FindElement(ParentBy);
            return childElement.FindElement(By.XPath("./*"));
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var childElement = context.FindElement(ParentBy);
            return childElement.FindElements(By.XPath("./*"));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByChildOf([{0}])", ParentBy);
        }
    }
}