using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    //TODO - add appropriate exceptions for no results
    internal class ByChildrenOf : By
    {
        By ParentBy;

        public ByChildrenOf(By parentBy)
        {
            ParentBy = parentBy;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var parentElement = context.FindElement(ParentBy);
            return parentElement.FindElement(By.XPath("./*"));
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var parentElement = context.FindElement(ParentBy);
            return parentElement.FindElements(By.XPath("./*"));
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByChildOf([{0}])", ParentBy);
        }
    }
}