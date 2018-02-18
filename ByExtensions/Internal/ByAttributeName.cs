using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByAttributeName : By
    {
        string AttributeName;

        public ByAttributeName(string attributeName)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentException("The attributeName cannot be null or empty", "attributeName");
            }

            AttributeName = attributeName;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: attributeName = \"" + AttributeName + "\"");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            var allElements = By.CssSelector("*").FindElements(context);

            foreach (var element in allElements)
            {
                if (element.HasAttribute(AttributeName))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByAttributeName({0})", AttributeName);
        }
    }
}
