using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestFixture]
    public class DodawanieProduktu
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheDodawanieProduktuTest()
        {
            driver.Navigate().GoToUrl("http://localhost:63782/");
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("hehehe");
            driver.FindElement(By.Id("Password")).Click();
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456");
            driver.FindElement(By.CssSelector("input.btn.btn-success")).Click();
            driver.FindElement(By.LinkText("Produkty")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-success")).Click();
            driver.FindElement(By.Id("Description")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Truskawka");
            driver.FindElement(By.Id("Kcal")).Clear();
            driver.FindElement(By.Id("Kcal")).SendKeys("10");
            driver.FindElement(By.Id("Protein")).Clear();
            driver.FindElement(By.Id("Protein")).SendKeys("5");
            driver.FindElement(By.Id("Carbs")).Clear();
            driver.FindElement(By.Id("Carbs")).SendKeys("7");
            driver.FindElement(By.Id("Fat")).Clear();
            driver.FindElement(By.Id("Fat")).SendKeys("8");
            driver.FindElement(By.CssSelector("input.btn.btn-success")).Click();
            try
            {
                Assert.AreEqual("Truskawka", driver.FindElement(By.XPath("//div[7]/div[2]/div/div/h3/strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
