using System;
using OpenQA.Selenium;
using FlexibleSelenium.IWebElementExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class IWebElementExtensionsTests : BaseTest
    {

        //GetXPath Tests
        [Test]
        [Category("GetXPath")]
        public void GetXPath_Every_Element_Can_Get_Itself()
        {
            SetUp();

            var allElements = Driver.FindElements(By.CssSelector("*"));

            foreach (var element in allElements)
            {
                var elementXpath = element.GetXPath();

                var elementByXpath = Driver.FindElement(By.XPath(elementXpath));

                Assert.AreEqual(element, elementByXpath, "Broke on elementXpath: " + elementXpath);
            }
        }

        //Relational Bool Tests
        [Test]
        [Category("Relational Bools")]
        public void IsDescendantOf_Simple_True()
        {
            SetUp();

            var h2Element = Driver.FindElement(By.Id("firstH2"));
            var subElement = Driver.FindElement(By.CssSelector("h2 sub"));

            Assert.IsTrue(subElement.IsDescendantOf(h2Element));
        }

        [Test]
        [Category("Relational Bools")]
        public void IsAncestorOf_Simple_True()
        {
            SetUp();

            var h2Element = Driver.FindElement(By.Id("firstH2"));
            var subElement = Driver.FindElement(By.CssSelector("h2 sub"));

            Assert.IsTrue(h2Element.IsAncestorOf(subElement));
        }

        //Descenants and Children tests
        [Test]
        [Category("Children and Descendants")]
        public void GetDescendants_Entire_Page()
        {
            SetUp();

            var htmlElement = Driver.FindElement(By.TagName("html"));

            var cssList = htmlElement.FindElements(By.CssSelector("*"));
            var resultList = htmlElement.GetDescendants();

            Assert.AreEqual(cssList, resultList);
        }

        [Test]
        [Category("Children and Descendants")]
        public void GetDescendants_Does_Not_Count_Text_Nodes()
        {
            SetUp();

            var div = Driver.FindElement(By.Id("div1"));

            var descendants = div.GetDescendants();

            Assert.AreEqual(4, descendants.Count);
        }

        //HasAttribute tests

        [Test]
        [Category("HasAttribute")]
        public void HasAttribute_No_Attributes_On_Element()
        {
            SetUp();

            var element = Driver.FindElement(By.TagName("h1"));

            Assert.IsFalse(element.HasAttribute("href"));
        }       
    }
}