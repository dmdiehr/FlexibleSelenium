using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlexibleSelenium.IWebElementExtensions
{
    public static class IWebElementExtensions
    {
        /// <summary>
        /// Returns a string representation of an XPath query that can be used to locate this IWebElement.
        /// </summary>
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

        /// <summary>
        /// Returns a the collection of children elements of this IWebElement
        /// </summary>
        public static ReadOnlyCollection<IWebElement> GetChildren(this ISearchContext context)
        {
            return context.FindElements(By.XPath("./*"));
        }

        /// <summary>
        /// Returns the child element of this ISearchContext that corresponds to the provided childIndex argument.
        /// </summary>
        public static IWebElement GetChild(this ISearchContext context, int childIndex)
        {
            var children = context.GetChildren();

            if (children.Count <= childIndex)
                throw new NoSuchElementException("Unable to locate an child element: ChildIndex = " + childIndex + " The context element has a children count of " + children.Count);
            return context.GetChildren()[childIndex];
        }

        /// <summary>
        /// Returns the number of children of this ISearchContext
        /// </summary>
        public static int ChildCount(this ISearchContext context)
        {
            return context.GetChildren().Count;
        }

        /// <summary>
        /// Returns a collection of all the descendant elements of this ISearchContext
        /// </summary>
        public static ReadOnlyCollection<IWebElement> GetDescendants(this ISearchContext context)
        {
            return context.FindElements(By.XPath(".//*"));
        }

        /// <summary>
        /// Returns a specific descendant element of this ISearchContext. The element is located by stepping through each generation at the childIndex for that generation as provided by the pathIndeces argument. 
        /// </summary>
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

        /// <summary>
        /// Returns the total number of descendants of this ISearchContext.
        /// </summary>
        public static int DescendantCount(this ISearchContext context)
        {
            return context.GetDescendants().Count;
        }

        /// <summary>
        /// Tests whether the provided IWebElement is indeed a descendant element of this IWebElement.
        /// </summary>
        public static bool IsDescendantOf(this IWebElement supposedDescendant, IWebElement supposedAncestor)
        {
            var allDescendants = supposedAncestor.GetDescendants();
            return allDescendants.Contains(supposedDescendant);
        }

        /// <summary>
        /// Tests whether the provided IWebElement is indeed an ancestor element of this IWebElement.
        /// </summary>
        public static bool IsAncestorOf(this IWebElement supposedAncestor, IWebElement supposedDescendant)
        {
            var allDescendants = supposedAncestor.GetDescendants();
            return allDescendants.Contains(supposedDescendant);
        }

        /// <summary>
        /// Tests whether this IWebElement has an attribute (with a non empty value) that matches the provided attributeName argument.
        /// </summary>
        public static bool HasAttribute(this IWebElement element, string attributeName)
        {
            return !String.IsNullOrWhiteSpace(element.GetAttribute(attributeName));
        }
    }
}
