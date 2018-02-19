using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using FlexibleSelenium.StaticDriver;

namespace FlexibleSelenium.Tests
{
    public abstract class BaseTest
    {
        public void SetUp(string driver = "chrome", int waitMilliseconds = 2000, string url = @"https://dmdiehr.github.io/TestHTML/")
        {
            Driver.Initialize(driver, waitMilliseconds, url, "user", "password");
            Driver.GoToUrl(url);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Driver.Close();
        }
    }
}