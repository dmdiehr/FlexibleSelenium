using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByAttributeValue : By
    {
        string AttributeName;
        string AttributeValue;

        public ByAttributeValue(string attributeName, string attributeValue)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentException("The attributeName cannot be null or empty", "attributeName");
            }

            if (string.IsNullOrEmpty(attributeValue))
            {
                throw new ArgumentException("The attributeValue cannot be null or empty", "attributeValue");
            }
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: attributeName = \"" + AttributeName + "\" attributeValue = \"" + AttributeValue + "\"");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            var allElements = By.CssSelector("*").FindElements(context);

            foreach (var element in allElements)
            {

                if (element.HasAttribute(AttributeName) && element.GetAttribute(AttributeName).Equals(AttributeValue))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByAttributeValue([{0}, {1}])", AttributeName, AttributeValue);
        }
    }
}
