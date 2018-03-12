using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByTextAndTag : By
    {
        private string TextToFind;
        private string[] Tags;
        private StringComparison Comparison;

        internal ByTextAndTag(string textToFind, params string[] tags)
        {
            if (string.IsNullOrEmpty(textToFind))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "textToFind");
            }
            if (tags.Length < 1)
            {
                throw new ArgumentException("There must be at least one non-empty tag provided; if the tag type is unneccesary use the ByText class", "tags");
            }

            Comparison = StringComparison.OrdinalIgnoreCase;
            TextToFind = textToFind;
            Tags = tags;
        }

        public ByTextAndTag(StringComparison comparison, string textToFind, params string[] tags)
        {
            if (string.IsNullOrEmpty(textToFind))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "textToFind");
            }
            if (tags.Length < 1)
            {
                throw new ArgumentException("There must be at least one non-empty tag provided; if the tag type is unneccesary use the ByText class", "tags");
            }

            Comparison = comparison;
            TextToFind = textToFind;
            Tags = tags;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element: TextToFind = \"" + TextToFind + "\" within the given tag types");
            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            string targetText = TextToFind.Trim();

            List<IWebElement> elementsByTag = new List<IWebElement>();

            foreach (string tag in Tags)
            {
                elementsByTag.AddRange(By.CssSelector(tag).FindElements(context));
            }

            foreach (var element in elementsByTag)
            {
                string elementText = element.Text.Trim(); //Despite the definition of IWebElement.Text, I have seen EdgeDriver return untrimmed strings.

                if (targetText.Equals(elementText, Comparison))
                {
                    elementCollection.Add(element);
                }
            }
            return elementCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByTextAndTag([{0}, {1}])", TextToFind, Tags);
        }
    }
}
