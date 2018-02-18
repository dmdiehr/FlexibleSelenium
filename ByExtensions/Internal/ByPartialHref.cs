using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByPartialHref : By
    {
        string PartialHref;

        public ByPartialHref(string partialHref)
        {
            if (string.IsNullOrEmpty(partialHref))
            {
                throw new ArgumentException("The partialHref cannot be null or empty", "partialHref");
            }
            PartialHref = partialHref;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: partialHref = \"" + PartialHref + "\"");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            var allAnchors = By.TagName("a").FindElements(context);

            foreach (var element in allAnchors)
            {

                if (element.HasAttribute("href") && element.GetAttribute("href").Contains(PartialHref))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByPartialHref([{0}])", PartialHref);
        }
    }
}
