using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FlexibleSelenium.IWebElementExtensions
{
    public static class IWebElementExtensions
    {
        public static string GetXPath(this IWebElement element)
        {
            return "//" + XPathBuilder(element, "");
        }

        private static string XPathBuilder(this IWebElement element, string subString)
        {
            //check if the element has an ID
            //if yes, prepend the xpath ID to the subString and return it
            string id = element.GetAttribute("id");
            if (!String.IsNullOrEmpty(id))
                return "*[@id =\"" + id + "\"]" + subString;


            //check if the element is one of the base elements
            //if yes, prepend the tagname to the subString and return it
            string tag = element.TagName;
            if (
                    tag.Equals("html", StringComparison.OrdinalIgnoreCase) ||
                    tag.Equals("body", StringComparison.OrdinalIgnoreCase) ||
                    tag.Equals("head", StringComparison.OrdinalIgnoreCase)
               )
            {
                return tag + subString;
            }

            //otherwise, find the element's tag name, and position in it's generation
            //prepend that to the subString
            //and recall this method on the parent element and the new subString

            IWebElement parent = element.FindElement(By.XPath(".."));
            ReadOnlyCollection<IWebElement> siblingsOfSameType = parent.FindElements(By.XPath("./" + tag));

            int positionAmongSiblings = 0;

            for (int i = 0; i < siblingsOfSameType.Count; i++)
            {
                if (element.Equals(siblingsOfSameType[i]))
                {
                    positionAmongSiblings = i + 1; //XPath indexes start at 1
                    break;
                }
            }

            if (positionAmongSiblings == 0)
                throw new ApplicationException("Something went wrong in the XPathBuilder method. The position of an element among its siblings could not be found");

            subString = "/" + tag + "[" + positionAmongSiblings + "]" + subString;
            return XPathBuilder(parent, subString);
        }

        public static ReadOnlyCollection<IWebElement> GetChildren(this ISearchContext context)
        {
            return context.FindElements(By.XPath("./*"));
        }

        public static IWebElement GetChild(this ISearchContext context, int childIndex)
        {
            return context.GetChildren()[childIndex];
        }

        public static int ChildCount(this ISearchContext context)
        {
            return context.GetChildren().Count;
        }

        public static ReadOnlyCollection<IWebElement> GetDescendants(this ISearchContext context)
        {
            return context.FindElements(By.XPath(".//*"));
        }

        public static IWebElement GetDescendant(this ISearchContext context, params int[] pathIndeces)
        {
            if (pathIndeces.Length < 1)
            {
                throw new ArgumentException("The pathIndeces params cannot be empty");
            }

            if (!pathIndeces.All(x => x >= 0))
            {
                throw new ArgumentException("All pathIndeces values must be non-negative");
            }

            //Since I can't create an empty IWebElement, I do the first step through the pathIndeces to get the element in the sequence.
            IWebElement returnElement = context.GetChild(pathIndeces[0]);

            for (int i = 1; i < pathIndeces.Length; i++)
            {
                returnElement = returnElement.GetChild(pathIndeces[i]);
            }

            return returnElement;
        }

        public static int DescendantCount(this ISearchContext context)
        {
            return context.GetDescendants().Count;
        }

        public static bool IsDescendantOf(this IWebElement supposedDescendant, IWebElement supposedAncestor)
        {
            var allDescendants = supposedAncestor.GetDescendants();
            return allDescendants.Contains(supposedDescendant);
        }

        public static bool IsAncestorOf(this IWebElement supposedAncestor, IWebElement supposedDescendant)
        {
            var allDescendants = supposedAncestor.GetDescendants();
            return allDescendants.Contains(supposedDescendant);
        }

        public static bool HasAttribute(this IWebElement element, string attributeName)
        {
            return !String.IsNullOrWhiteSpace(element.GetAttribute(attributeName));
        }
    }
}
