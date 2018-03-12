using FlexibleSelenium.PageElements;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByChildrenOf : By
    {
        PageElement ParentElement;
        int ChildIndex;

        internal ByChildrenOf(By parentBy, int childIndex)
        {
            ChildIndex = childIndex;
            ParentElement = new PageElement(parentBy);
        }

        internal ByChildrenOf(PageElement parentElement, int childIndex)
        {
            ChildIndex = childIndex;
            ParentElement = parentElement;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count <= ChildIndex)
                throw new NoSuchElementException("Unable to locate an child element: ChildIndex = " + ChildIndex + " The element located with the parentBy parameter had " + elements.Count + " children elements");

            return FindElements(context)[ChildIndex];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ParentElement.FindElements(By.XPath("./*"));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByChildOf([{0}])", ParentElement);
        }
    }
}