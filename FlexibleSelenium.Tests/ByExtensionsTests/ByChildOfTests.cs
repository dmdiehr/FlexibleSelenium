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
    public class ByChildOfTests : BaseTest
    {
        [TestCase("chrome")]
        [TestCase("firefox")]
        [TestCase("edge")]
        public void ByChildOf_Simple_Crossbrowser(string driver)
        {
            SetUp(driver);

            var parentElement = Driver.FindElement(By.TagName("ol"));
            var secondChild = parentElement.FindElement(By.XPath("./li[2]"));

            var resultChild = Driver.FindElement(ByExtension.ChildOf(By.TagName("ol"), 1));

            Assert.AreEqual(secondChild, resultChild);

        }

        [Test]
        public void ChildOf_Out_Of_Range_Throws_No_Such_Element_Exception()
        {
            SetUp();

            var e = new Exception();
 
            try
            {
                var resultChild = Driver.FindElement(ByExtension.ChildOf(By.TagName("ol"), 5));
            }
            catch (Exception ex)
            {

                e = ex;
            }

            Assert.IsTrue(e is NoSuchElementException);
        }
    }
}