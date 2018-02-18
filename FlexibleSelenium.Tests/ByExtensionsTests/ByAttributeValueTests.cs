using System;
using OpenQA.Selenium;
using FlexibleSelenium.ByExtensions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace FlexibleSelenium.Tests
{
    [TestFixture]
    public class ByAttributeValueTests : BaseTest
    {
        [Test]
        public void ByAttributeValue_Element_With_One_Attribute()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("div4"));

            var resultElement = Driver.FindElement(ByExtension.AttributeValue("foobar", "foobar attribute"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByAttributeValue_No_Matching_Attribute_Gets_Exception()
        {
            SetUp();

            var ex = new Exception();

            try
            {
                var element = Driver.FindElement(ByExtension.AttributeValue("asdfg", "asdfdsa"));
            }
            catch (Exception e)
            {

                ex = e;
            }

            Assert.AreEqual("OpenQA.Selenium.NoSuchElementException", ex.GetType().ToString());
        }

        [Test]
        public void ByAttributeValue_No_Matching_Value_Gets_Exception()
        {
            SetUp();

            var ex = new Exception();

            try
            {
                var element = Driver.FindElement(ByExtension.AttributeValue("foobar", "adfasdf"));
            }
            catch (Exception e)
            {

                ex = e;
            }

            Assert.AreEqual("OpenQA.Selenium.NoSuchElementException", ex.GetType().ToString());
        }

        [Test]
        public void ByAttributeValue_Multiple_Matches()
        {
            SetUp();

            var expectedList = Driver.FindElements(By.CssSelector("ol li"));

            var resultList = Driver.FindElements(ByExtension.AttributeValue("dir", "rtl"));

            Assert.AreEqual(4, resultList.Count);
            Assert.AreEqual(expectedList, resultList);
        }
    }
}