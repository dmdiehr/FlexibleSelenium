using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByPartialTextAndTag : By
    {
        private string PartialTextToFind;
        private string[] Tags;

        public ByPartialTextAndTag(string partialTextToFind, params string[] tags)
        {
            if (string.IsNullOrEmpty(partialTextToFind))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "partialText");
            }
            if (tags.Length < 1)
            {
                throw new ArgumentException("There must be at least one non-empty tag provided; if the tag type is unneccesary use the ByPartialText class", "tags");
            }

            PartialTextToFind = partialTextToFind;
            Tags = tags;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException("Unable to locate an element with partial text: partialTextToFind = \"" + PartialTextToFind + "\" within the given tag types");

            return elements[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> elementCollection = new List<IWebElement>();
            List<IWebElement> elementsByTag = new List<IWebElement>();

            foreach (string tag in Tags)
            {
                elementsByTag.AddRange(By.TagName(tag).FindElements(context));
            }

            foreach (var element in elementsByTag)
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
            return string.Format(CultureInfo.InvariantCulture, "ByPartialTextAndTag([{0}, {1}])", PartialTextToFind, Tags);
        }
    }
}
