using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Test
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://mrssts.hemit.org/";
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
        public void TheTest()
        {
            driver.Navigate().GoToUrl("https://mrsdev.helsemn.no/PromsTestregister/");
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("promstest");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("promstest");
            driver.FindElement(By.Id("singlebutton")).Click();
            driver.FindElement(By.Id("singlebutton")).Click();
            driver.FindElement(By.Id("SearchParameter")).Clear();
            driver.FindElement(By.Id("SearchParameter")).SendKeys("05110662450");
            driver.FindElement(By.Id("patientSearchBtn")).Click();
            driver.FindElement(By.XPath("//div[@id='main']/div[4]/div/button")).Click();
            driver.FindElement(By.XPath("//div[@id='navbar']/ul/li/div/a")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"deleteForm('9dfddc57-e8ff-414e-b1e7-a53ac3b7b943')\"]")).Click();
            driver.FindElement(By.CssSelector("div.modal-footer > button.btn.btn-primary")).Click();
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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
