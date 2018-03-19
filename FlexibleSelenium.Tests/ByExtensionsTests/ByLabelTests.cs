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
    public class ByLabelTests : BaseTest
    {
        [TestCase("chrome")]
        [TestCase("firefox")]
        [TestCase("edge")]
        public void ByLabel_Simple_Label_By_Crossbrowser(string driver)
        {
            SetUp(driver);

            var expectedElement = Driver.FindElement(By.Id("next_input"));
            var resultElement = Driver.FindElement(ByExtension.Label(By.Id("for_next_input")));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByLabel_Simple_Label_Text()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("next_input"));
            var resultElement = Driver.FindElement(ByExtension.Label("label for next input"));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByLabel_Embedded_Label_By()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("embedded_input"));
            var resultElement = Driver.FindElement(ByExtension.Label(By.Id("for_embedded_input")));

            Assert.AreEqual(expectedElement, resultElement);
        }

        [Test]
        public void ByLabel_Embedded_Label_Text()
        {
            SetUp();

            var expectedElement = Driver.FindElement(By.Id("embedded_input"));
            var resultElement = Driver.FindElement(ByExtension.Label("label for embedded input"));

            Assert.AreEqual(expectedElement, resultElement);
        }
    }
}
