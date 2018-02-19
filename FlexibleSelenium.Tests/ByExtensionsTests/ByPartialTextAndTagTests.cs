using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByPartialTextAndTagTests : BaseTest
    {
        [Test]
        public void ByPartialTextAndTag_Single_Full_Match()
        {
            SetUp();
            var expectedElement = Driver.FindElement(By.LinkText("Page 1"));

            var resultElement = Driver.FindElement(ByExtension.PartialTextAndTag("Page 1", "a"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByPartialTextAndTag_Multiple_Tags_Full_Match()
        {
            SetUp();
            var expectedATag = Driver.FindElement(By.LinkText("Page 1"));
            var expectedLiTag = Driver.FindElement(By.Id("navP1"));

            var resultList = Driver.FindElements(ByExtension.PartialTextAndTag("Page 1", "a", "li"));

            Assert.AreEqual(2, resultList.Count);
            Assert.Contains(expectedATag, resultList);
            Assert.Contains(expectedLiTag, resultList);
        }

        [Test]
        public void ByPartialTextAndTag_Partial_Match_Multiple_Results()
        {
            SetUp();

            var resultList1 = Driver.FindElements(ByExtension.PartialTextAndTag("Page", "a", "li"));

            Assert.AreEqual(8, resultList1.Count);
        }

        [Test]
        public void ByPartialTextAndTag_No_Matching_Text_Throws_NoSuchElementException()
        {
            SetUp();
            var ex = new Exception();

            try
            {
                var element = Driver.FindElement(ByExtension.PartialTextAndTag("asdfsdafsadf", "div"));
            }
            catch (Exception e)
            {
                ex = e;               
            }
            
            Assert.AreEqual("OpenQA.Selenium.NoSuchElementException", ex.GetType().ToString());
        }

        [Test]
        public void ByPartialTextAndTag_Matching_Text_But_Not_Tag()
        {
            SetUp();

            var resultList = Driver.FindElements(ByExtension.PartialTextAndTag("Page", "span"));

            Assert.AreEqual(0, resultList.Count);
        }

        [Test]
        public void ByPartialTextAndTag_No_Matching_Tag()
        {
            SetUp();

            //This is checking to make sure the test is valid, in case the test site gets updated to include a td element.
            Assert.AreEqual(0, Driver.FindElements(By.TagName("td")).Count);

            var resultList = Driver.FindElements(ByExtension.PartialTextAndTag("Page", "td"));

            Assert.AreEqual(0, resultList.Count);
        }

    }
}