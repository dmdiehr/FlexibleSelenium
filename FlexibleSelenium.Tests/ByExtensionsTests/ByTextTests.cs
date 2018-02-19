using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByTextTests : BaseTest
    {
        [TestCase("chrome")]
        [TestCase("firefox")]
        [TestCase("edge")]
        public void ByText_Simple_Crossbrowser(string driver)
        {
            SetUp(driver);

            var expectedElement = Driver.FindElement(By.Id("secondH2"));
            var resultElement = Driver.FindElement(ByExtension.Text("sub-heading"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByText_Case_Sensitivity_Simple()
        {
            SetUp();

            var caseSentsitiveList = Driver.FindElements(ByExtension.Text("list item", StringComparison.InvariantCulture));
            Assert.AreEqual(0, caseSentsitiveList.Count);

            var caseInsentsitiveList = Driver.FindElements(ByExtension.Text("list item", StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(8, caseInsentsitiveList.Count);
        }

        [Test]
        public void ByText_Many_Matches()
        {
            SetUp();

            var resultList = Driver.FindElements(ByExtension.Text("List item"));
            Assert.AreEqual(8, resultList.Count);
        }

        [Test]
        public void ByText_One_Of_Many()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("firstLi"));
            var resultElement = Driver.FindElement(ByExtension.Text("List item"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByText_Returns_Innermost_Element()
        {
            SetUp();

            var bothElements = Driver.FindElements(ByExtension.Text("Page 1"));
            var oneElement = Driver.FindElement(ByExtension.Text("Page 1"));

            Assert.AreEqual(2, bothElements.Count);
            Assert.AreEqual("a", oneElement.TagName);
        }
    }
}
