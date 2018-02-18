using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByPartialTextTests : BaseTest
    {
        [Test]
        public void ByPartialText_Single_Occurence_Full_Match()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.TagName("time"));
            var resultElement = Driver.FindElement(ByExtension.PartialText("2018"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByPartialText_Single_Occurence_Middle_Word()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.CssSelector("footer div"));
            var resultElement = Driver.FindElement(ByExtension.PartialText("footer"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByPartialText_Ancestors_Are_In_List()
        {
            SetUp();

            var resultList = Driver.FindElements(ByExtension.PartialText("2018"));

            Assert.AreEqual(5, resultList.Count);
            Assert.AreEqual(resultList[1].TagName, "small");
        }
    }
}
