using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByPartialHrefTests : BaseTest
    {
        [Test]
        public void ByPartialHref_Entire_Href()
        {
            SetUp();

            var page1Link = Driver.FindElement(By.LinkText("Page 1"));
            var resultElement = Driver.FindElement(ByExtension.PartialHref("https://dmdiehr.github.io/test/page1"));

            Assert.AreEqual(page1Link, resultElement);
        }

        [Test]
        public void ByPartialHref_One_Match()
        {
            SetUp();

            var page1Link = Driver.FindElement(By.LinkText("Page 2"));
            var resultElement = Driver.FindElement(ByExtension.PartialHref("/test/page2"));

            Assert.AreEqual(page1Link, resultElement);
        }

        [Test]
        public void ByPartialHref_Many_Matches()
        {
            SetUp();

            var pageLinks = Driver.FindElements(ByExtension.PartialHref("page"));

            Assert.AreEqual(4, pageLinks.Count);
        }
    }
}