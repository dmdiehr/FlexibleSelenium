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
    public class ByChildrenOfTests : BaseTest
    {
        [TestCase("chrome")]
        [TestCase("firefox")]
        [TestCase("edge")]
        public void ByChildrenOf_Simple_Crossbrowser(string driver)
        {
            SetUp(driver);

            var olList = Driver.FindElements(By.CssSelector("ol li"));
            Assert.AreEqual(4, olList.Count);

            var resultList = Driver.FindElements(ByExtension.ChildrenOf(By.TagName("ol")));

            Assert.AreEqual(olList , resultList);
        }
    }
}