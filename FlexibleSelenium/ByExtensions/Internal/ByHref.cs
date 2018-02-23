using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByHref : By
    {
        string Href;

        public ByHref(string href)
        {
            if (string.IsNullOrEmpty(href))
            {
                throw new ArgumentException("The href cannot be null or empty", "href");
            }
            Href = href;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: href = \"" + Href + "\"");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            var allAnchors = By.TagName("a").FindElements(context);

            foreach (var element in allAnchors)
            {

                if (element.HasAttribute("href") && element.GetAttribute("href").Equals(Href))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByHref([{0}])", Href);
        }
    }
}
