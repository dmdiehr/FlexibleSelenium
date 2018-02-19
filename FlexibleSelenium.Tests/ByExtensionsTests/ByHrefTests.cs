using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByHrefTests : BaseTest
    {
        [Test]
        public void ByHref_Simple()
        {
            SetUp();

            var page1Link = Driver.FindElement(By.LinkText("Page 1"));
            var resultElement = Driver.FindElement(ByExtension.Href("https://dmdiehr.github.io/test/page1"));

            Assert.AreEqual(page1Link, resultElement);
        }        
    }
}