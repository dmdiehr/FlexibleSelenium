using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace FlexibleSelenium.Tests
{
    public abstract class BaseTest
    {
        public void SetUp(string driver = "chrome", string url = @"https://dmdiehr.github.io/TestHTML/")
        {
            Driver.Initialize(driver);
            Driver.GoToUrl(url);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Driver.Close();
        }
    }
}