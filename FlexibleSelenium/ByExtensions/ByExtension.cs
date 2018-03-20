using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions.Internal;
using FlexibleSelenium.PageElements;

namespace FlexibleSelenium.ByExtensions
{
    public class ByExtension
    {
        /// <summary>
        /// Gets a mechanism to find elements by a name of one of their attributes.
        /// </summary>
        public static By AttributeName(string attributeName)
        {
            return new ByAttributeName(attributeName);
        }

        /// <summary>
        /// Gets a mechanism to find elements by the name and value of one of their attributes.
        /// </summary>
        public static By AttributeValue(string attributeName, string attributeValue)
        {
            return new ByAttributeValue(attributeName, attributeValue);
        }

        /// <summary>
        /// Gets a mechanism to find elements by the value of their href attribute.
        /// </summary>
        public static By Href(string href)
        {
            return new ByHref(href);
        }

        /// <summary>
        /// Gets a mechanism to find elements by partially matching the value of their href attribute.
        /// </summary>
        public static By PartialHref(string partialHref)
        {
            return new ByPartialHref(partialHref);
        }

        /// <summary>
        /// Gets a mechanism to find elements by partially matching their text. Priority is given (i.e. Collection order and first returned element) to elements with the least text. This should usually return the closest partial match. (If the first partial match was returned the root element of the context would always be returned first.)
        /// </summary>
        public static By PartialText(string partialTextToFind)
        {
            return new ByPartialText(partialTextToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by partial text, delimited to elements with tags within the provided tags argument.
        /// </summary>
        public static By PartialTextAndTag(string partialTextToFind, params string[] tags)
        {
            return new ByPartialTextAndTag(partialTextToFind, tags);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their text value. Text is always trimmed of whitespace before comparison. Comparison is done based on the provided StringComparison argument.
        /// </summary>
        public static By Text(string textToFind, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return new ByText(textToFind, comparison);
        }

        /// <summary>
        /// Gets a mechanism to find elements by text, delimited to elements with tags within the provided tags argument. Text is always trimeed of whitespace before comparison. Comparison is 'OrdinalIgnoreCase' by default.
        /// </summary>
        public static By TextAndTag(string text, params string[] tags)
        {
            return new ByTextAndTag(text, tags);
        }

        /// <summary>
        /// Gets a mechanism to find elements by text, delimited to elements with tags within the provided tags argument. Text is always trimeed of whitespace before comparison.  Comparison is done based on the provided StringComparison argument.
        /// </summary>
        public static By TextAndTag(StringComparison comparison, string text, params string[] tags)
        {
            return new ByTextAndTag(comparison, text, tags);
        }

        /// <summary>
        /// Gets a mechanism to find an element by the By of one of their child elements.
        /// </summary>
        public static By ParentOf(By childBy)
        {
            return new ByParentOf(childBy);
        }

        /// <summary>
        /// Gets a mechanism to find an element by the PageElement of one of their child elements.
        /// </summary>
        public static By ParentOf(PageElement childElement)
        {
            return new ByParentOf(childElement);
        }

        /// <summary>
        /// Gets a mechanism to find the children of the element found with the provided By argument.
        /// </summary>
        /// <param name="parentElement">The element to search within</param>
        /// <param name="childIndex">The index of the child element to return when using FindElement. This parameter is not appropriate for the FindElements method and will be ignored when used in that case.</param>
        public static By ChildrenOf(By parentBy, int childIndex = 0)
        {
            return new ByChildrenOf(parentBy, childIndex);
        }

        /// <summary>
        /// Gets a mechanism to find the children of the provided PageElement argument.
        /// </summary>
        /// <param name="parentElement">The element to search within</param>
        /// <param name="childIndex">The index of the child element to return when using FindElement. This parameter is not appropriate for the FindElements method and will be ignored when used in that case.</param>
        public static By ChildrenOf(PageElement parentElement, int childIndex = 0)
        {
            return new ByChildrenOf(parentElement, childIndex);
        }

        /// <summary>
        /// Gets a mechanims to find an element by its label.
        /// </summary>
        /// <param name="labelText">The text used to find the label. The comparison is always OrdinalIgnoreCase</param>
        /// <returns>The element referenced by or contained within the label element.</returns>
        public static By Label(string labelText)
        {
            return new ByLabel(labelText);
        }

        /// <summary>
        /// Gets a mechanims to find an element by its label.
        /// </summary>
        /// <param name="labelBy">The By to find the label</param>
        /// <returns>The element referenced by or contained within the label element.</returns>
        public static By Label(By labelBy)
        {
            return new ByLabel(labelBy);
        }
    }
}
