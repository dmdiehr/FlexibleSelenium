using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByText : By
    {
        private string TextToFind;
        StringComparison Comparison;

        internal ByText(string textToFind, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(textToFind))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "textToFind");
            }

            TextToFind = textToFind.Trim();
            Comparison = comparison;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: TextToFind = \"" + TextToFind + "\"");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            string targetText = TextToFind.Trim();
            var allElements = By.CssSelector("*").FindElements(context);


            foreach (var element in allElements)
            {
                string elementText = element.Text.Trim(); //Despite the definition of IWebElement.Text, I have seen EdgeDriver return untrimmed strings.

                if (targetText.Equals(elementText, Comparison))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.OrderBy(e => e.ChildCount()).ToList().AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByText([{0}])", TextToFind);
        }
    }
}