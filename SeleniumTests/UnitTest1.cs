using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        [SetUp]
        public void Init()
        {
            driver = new FirefoxDriver();
        }

        [Test]
        public void OpenUp()
        {
            driver.Url = @"http://localhost:63782/";
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}
