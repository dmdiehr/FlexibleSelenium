using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByPartialText : By
    {
        private string PartialTextToFind;

        internal ByPartialText(string partialTextToFind, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(partialTextToFind))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "partialTextToFind");
            }

            PartialTextToFind = partialTextToFind;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element with partial text: partialTextToFind = \"" + PartialTextToFind + "\"");

            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            var allElements = By.CssSelector("*").FindElements(context);


            foreach (var element in allElements)
            {
                if (element.Text.Contains(PartialTextToFind))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.OrderBy(e => e.Text.Length).ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByPartialText([{0}])", PartialTextToFind);
        }
    }
}
