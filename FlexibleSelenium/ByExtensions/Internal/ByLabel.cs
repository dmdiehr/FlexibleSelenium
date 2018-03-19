using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FlexibleSelenium.ByExtensions.Internal
{
    internal class ByLabel : By
    {       
        private By LabelBy;
        private string NoSuchElementMessage;

        internal ByLabel(string labelText)
        {
            if (string.IsNullOrEmpty(labelText))
            {
                throw new ArgumentException("The text to find cannot be null or empty", "textToFind");
            }

            LabelBy = ByExtension.TextAndTag(labelText, "label");
        }

        internal ByLabel(By labelBy)
        {
            LabelBy = labelBy;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0 && !String.IsNullOrWhiteSpace(NoSuchElementMessage))
                throw new NoSuchElementException(NoSuchElementMessage);
            else if (elements.Count == 0)
            {
                throw new NoSuchElementException();
            }
            else
                return FindElements(context)[0];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> returnCollection = new List<IWebElement>();
            var allMatchingLabelElements = context.FindElements(LabelBy);

            if (allMatchingLabelElements.Count == 0)
            {
                NoSuchElementMessage = "No matching label elements were locateable: " + LabelBy;
                return returnCollection.AsReadOnly();
            }

            foreach (var element in allMatchingLabelElements)
            {

                try
                {
                    var forValue = element.GetAttribute("for");
                    var referencedElement = context.FindElement(By.Id(forValue));
                    returnCollection.Add(referencedElement);
                    continue;
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is ArgumentNullException)
                {
                    //if the label element has no 'for' attribute, then we should try to find the element embedded within the label
                    try
                    {
                        var childOfLabel = element.GetChild(0);
                        returnCollection.Add(childOfLabel);
                        continue;
                    }
                    catch (NoSuchElementException)
                    {

                        NoSuchElementMessage = "A <label> element was able to be found: " + LabelBy + " but the referenced element could not be found via the value of the 'for' attribute nor through nesting";
                    }

                }
            }
            return returnCollection.AsReadOnly();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByLabel([{0}])", LabelBy);
        }
    }
}