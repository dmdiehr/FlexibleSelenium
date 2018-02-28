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
    public class ByParentOfTests : BaseTest
    {
        [TestCase("chrome")]
        [TestCase("firefox")]
        [TestCase("edge")]
        public void ByParent_Simple_Crossbrowser(string driver)
        {
            SetUp(driver);

            var expectedElement = Driver.FindElement(By.TagName("ul"));
            var resultElement = Driver.FindElement(ByExtension.ParentOf(By.Id("navP1")));

            Assert.AreEqual(expectedElement, resultElement);
        }
    }
}