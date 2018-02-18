using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using FlexibleSelenium.PageElements;
using OpenQA.Selenium;

namespace FlexibleSelenium.Tests
{
    /// <summary>
    /// Summary description for PageElementTests
    /// </summary>
    [TestFixture]
    public class PageElementTests : BaseTest
    {
        //Ctor Tests

        [Test]
        public void PageElement_Ctor_No_Context()
        {
            SetUp();
            IWebElement expectedElement = Driver.FindElement(By.TagName("nav"));
            PageElement resultElement = new PageElement(Driver.Instance, By.TagName("nav"));

            Assert.AreEqual(expectedElement, resultElement.BaseElement);
        }

        [Test]
        public void PageElement_Ctor_With_ContextBy()
        {
            SetUp();
            IWebElement expectedElement = Driver.FindElement(By.Id("p2"));
            PageElement resultElement = new PageElement(Driver.Instance, By.Id("div1"), By.TagName("p"));

            Assert.AreEqual(expectedElement, resultElement.BaseElement);
        }

        //IsPresent Tests

        [Test]
        public void IsPresent_Simple_True()
        {
            SetUp();

            var element = new PageElement(Driver.Instance, By.TagName("nav"));

            Assert.IsTrue(element.IsPresent());
        }

        [Test]
        public void IsPresent_Simple_False()
        {
            SetUp();

            var element = new PageElement(Driver.Instance, By.TagName("nasdfav"));

            Assert.IsFalse(element.IsPresent());
        }

    }
}
