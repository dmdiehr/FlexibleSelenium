using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByAttributeNameTests : BaseTest
    {
        [Test]
        public void ByAttributeName_Two_Matches()
        {
            SetUp();

            var title1 = Driver.FindElement(By.Id("div1"));
            var title2 = Driver.FindElement(By.Id("div2"));

            var titleElements = Driver.FindElements(ByExtension.AttributeName("title"));

            Assert.AreEqual(2, titleElements.Count);
            Assert.Contains(title1, titleElements);
            Assert.Contains(title2, titleElements);
        }

        [Test]
        public void ByAttributeName_One_Match()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("div2"));

            var resultElement = Driver.FindElement(ByExtension.AttributeName("data-tip"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByAttributeName_Weird_Attribute()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("div4"));

            var resultElement = Driver.FindElement(ByExtension.AttributeName("foobar"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByAttributeName_No_Matches_Gets_Exception()
        {
            SetUp();

            var ex = new Exception();

            try
            {
                var element = Driver.FindElement(ByExtension.AttributeName("asdfg"));
            }
            catch (Exception e)
            {

                ex = e;
            }

            Assert.AreEqual("OpenQA.Selenium.NoSuchElementException", ex.GetType().ToString());
        }
    }
}