using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByTextAndTagTests : BaseTest
    {
        [Test]
        public void ByTextAndTag_Simple()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.LinkText("Page 1"));

            var resultElement = Driver.FindElement(ByExtension.TextAndTag("Page 1", "a"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByTextAndTag_Unfound_Text()
        {
            SetUp();

            var foundElements = Driver.FindElements(ByExtension.TextAndTag("Page 1", "p"));

            Assert.AreEqual(0, foundElements.Count);
        }

        [Test]
        public void ByTextAndTag_Parent_Element()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("navP4"));

            var resultElement = Driver.FindElement(ByExtension.TextAndTag("Page 4", "li"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByTextAndTag_Multiple_Matching_Tags()
        {
            SetUp();
            var aTag = Driver.FindElement(By.LinkText("Page 3"));
            var liTag = Driver.FindElement(By.Id("navP3"));

            var resultList = Driver.FindElements(ByExtension.TextAndTag("Page 3", "a", "li"));

            Assert.AreEqual(2, resultList.Count);
        }
    }
}