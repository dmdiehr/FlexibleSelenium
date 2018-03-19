using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions.Internal;
using FlexibleSelenium.PageElements;

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

        /// <summary>
        /// Finds the children of the provided element.
        /// </summary>
        /// <param name="parentElement">The element to search within</param>
        /// <param name="childIndex">The index of the child element to return when using FindElement. This parameter is not appropriate for the FindElements method and will be ignored when used in that case.</param>
        public static By ChildrenOf(By parentBy, int childIndex = 0)
        {
            return new ByChildrenOf(parentBy, childIndex);
        }

        /// <summary>
        /// Finds the children of the provided element.
        /// </summary>
        /// <param name="parentElement">The element to search within</param>
        /// <param name="childIndex">The index of the child element to return when using FindElement. This parameter is not appropriate for the FindElements method and will be ignored when used in that case.</param>
        public static By ChildrenOf(PageElement parentElement, int childIndex = 0)
        {
            return new ByChildrenOf(parentElement, childIndex);
        }

        public static By Label(string labelText)
        {
            return new ByLabel(labelText);
        }

        public static By Label(By labelBy)
        {
            return new ByLabel(labelBy);
        }
    }
}
