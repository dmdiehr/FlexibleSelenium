using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions.Internal;

namespace FlexibleSelenium.ByExtensions
{
    public class ByExtension
    {
        public static By AttributeName(string attributeName)
        {
            return new ByAttributeName(attributeName);
        }

        public static By AttributeValue(string attributeName, string attributeValue)
        {
            return new ByAttributeValue(attributeName, attributeValue);
        }

        public static By Href(string href)
        {
            return new ByHref(href);
        }

        public static By PartialHref(string partialHref)
        {
            return new ByPartialHref(partialHref);
        }

        public static By PartialText(string partialTextToFind)
        {
            return new ByPartialText(partialTextToFind);
        }

        public static By PartialTextAndTag(string partialTextToFind, params string[] tags)
        {
            return new ByPartialTextAndTag(partialTextToFind, tags);
        }

        /// <summary>
        /// Provides a By object capable of searching for elements based on their text. When used to locate elements it will return elements ordered by their descendant count in order to return the most specific elements first
        /// </summary>
        /// <param name="textToFind">The text used to locate the element</param>
        /// <param name="comparison">StringComparison type used when assessing string equality while looking for matching text</param>
        /// <returns> Returns a ByText instance</returns>
        public static By Text(string textToFind, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return new ByText(textToFind, comparison);
        }

        public static By TextAndTag(string text, params string[] tags)
        {
            return new ByTextAndTag(text, tags);
        }

        public static By ParentOf(By childBy)
        {
            return new ByParentOf(childBy);
        }

        public static By ChildrenOf(By parentBy)
        {
            return new ByChildrenOf(parentBy);
        }

        public static By ChildOf(By parentBy, int childIndex = 0)
        {
            return new ByChildOf(parentBy, childIndex);
        }
    }
}
