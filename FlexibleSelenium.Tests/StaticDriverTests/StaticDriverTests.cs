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

    [TestFixture]
    public class StaticDriverTests : BaseTest
    {
        [Test]
        public void AwaitElement_Throws_When_Element_Not_There()
        {
            SetUp();

            Exception e = new Exception();

            try
            {
                Driver.AwaitElement(By.Id("asdfasdf"));
            }
            catch (Exception ex)
            {

                e = ex;
            }
            Assert.IsTrue(e is AwaitElementException);
        }

        [Test]
        public void AwaitElement_Can_Throw_Custom_Message()
        {
            SetUp();

            Exception e = new Exception();

            try
            {
                Driver.AwaitElement(By.Id("asdfasdf"), "not finding 'asdfasdf'");
            }
            catch (Exception ex)
            {

                e = ex;
            }
            Assert.AreEqual("not finding 'asdfasdf'", e.Message);
        }

    }
}
