using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using FlexibleSelenium.PageElements;
using OpenQA.Selenium;
using FlexibleSelenium.StaticDriver;
using FlexibleSelenium.ByExtensions;
using System.Threading;

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
            PageElement resultElement = new PageElement(By.TagName("nav"));

            Assert.AreEqual(expectedElement, resultElement.BaseElement);
        }

        [Test]
        public void PageElement_Ctor_With_ContextBy()
        {
            SetUp();
            IWebElement expectedElement = Driver.FindElement(By.Id("p2"));
            PageElement resultElement = new PageElement(By.Id("div1"), By.TagName("p"));

            Assert.AreEqual(expectedElement, resultElement.BaseElement);
        }

        //IsPresent Tests

        [Test]
        public void IsPresent_Simple_True()
        {
            SetUp();            

            var element = new PageElement(By.TagName("nav"));

            Assert.IsTrue(element.IsPresent());
        }

        [Test]
        public void IsPresent_Simple_False()
        {
            SetUp();

            var element = new PageElement(By.TagName("nasdfav"));

            Assert.IsFalse(element.IsPresent());
        }

        [Test]
        public void Not_Visible_Exception_Caught()
        {
            SetUp("edge", 59000, "https://qaprod.kavi.com/higherlogic/mm/account#/accounts");

            var username = new PageElement(By.Id("username"));
            var password = new PageElement(By.Id("password"));
            var loginButton = new PageElement(ByExtension.AttributeValue("value", "Login"));

            username.Clear();
            username.SendKeys("diehrtest+sa1@gmail.com");
            password.Click();
            password.Clear();
            password.SendKeys("Tester1319");
            loginButton.Click();

            var logoutButton = new PageElement(ByExtension.PartialHref("logout"));

            logoutButton.Click();
        }

        [Test]
        [Category("PageElement.Contains")]
        public void Contains_With_PageElement_True()
        {
            SetUp();
            var parentElement = new PageElement(By.Id("div1"));
            var childElement = new PageElement(By.Id("p2"));

            Assert.IsTrue(parentElement.Contains(childElement));
        }

        [Test]
        [Category("PageElement.Contains")]
        public void Contains_With_By_True()
        {
            SetUp();
            var parentElement = new PageElement(By.Id("div1"));

            Assert.IsTrue(parentElement.Contains(By.Id("p2")));
        }

        [Test]
        [Category("PageElement.Contains")]
        public void Contains_With_PageElement_False()
        {
            SetUp();
            var parentElement = new PageElement(By.TagName("h1"));
            var childElement = new PageElement(By.Id("p2"));

            Assert.IsFalse(parentElement.Contains(childElement));
        }

        [Test]
        [Category("PageElement.Contains")]
        public void Contains_With_By_False()
        {
            SetUp();
            var parentElement = new PageElement(By.TagName("h1"));

            Assert.IsFalse(parentElement.Contains(By.Id("p2")));
        }
    }
}
