using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    //TODO - add appropriate exceptions for no results
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
            var parentElement = context.FindElement(ParentBy);
            var childrenList = parentElement.FindElements(By.XPath("./*"));

            if (childrenList.Count < ChildIndex)
                throw new NoSuchElementException("Unable to locate an child element: ChildIndex = " + ChildIndex + " The element located with the parentBy parameter had " + childrenList.Count + " children elements");
            return childrenList[ChildIndex];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            try
            {
                var element = FindElement(context);
                var singleChildList = new List<IWebElement> { element };
                return singleChildList.AsReadOnly();
            }
            catch (NoSuchElementException)
            {

                return new List<IWebElement>().AsReadOnly();
            }            
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByChildOf([{0}])", ParentBy);
        }
    }
}