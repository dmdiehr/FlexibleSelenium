using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByChildOf : By
    {
        By ParentBy;
        int ChildIndex;

        public ByChildOf(By parentBy, int childIndex)
        {
            ParentBy = parentBy;
            ChildIndex = childIndex;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var parentElement = context.FindElement(ParentBy);
            var childrenList = parentElement.FindElements(By.XPath("./*"));

            var singleChildList = new List<IWebElement> { childrenList[ChildIndex] };
            return singleChildList.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByChildOf([{0}])", ParentBy);
        }
    }
}