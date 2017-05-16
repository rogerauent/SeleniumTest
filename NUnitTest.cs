using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    public class NUnitTest
    {
        private static string host = "https://mrsdev.helsemn.no/";
        private static string appUrl = host + "PromsTestregister/";

        [TestFixtureSetUp]
        public void SetUpDrivers()
        {
            //driver = new FirefoxDriver();
        }

        [Test]
        public void TestChrome()
        {
            var driver = CreateChromeDriver();
            RunTest(driver);
        }

        [Test]
        public void TestEdge()
        {
            var driver = CreateEdgeDriver();
            RunTest(driver);
        }

        [Test]
        public void TestPhantomJs()
        {
            var driver = CreatePhantomJsDriver();
            RunTest(driver);
        }

        [Test]
        public void TestInternetExplorer()
        {
            var driver = CreateIEDriver();
            RunTest(driver);
        }

        private void RunTest(IWebDriver driver)
        {
            SignIn(driver);
            GUITest(driver);
            SignOut(driver);
            TearDown(driver);
        }

        private void GUITest(IWebDriver driver)
        {
            // Pasientsøk
            var pnr = "05110662450";
            //var s = "/PromsTestregister/MainForm/Create?patientInRegistryGuid=19a45d25-30f4-e611-9c67-80000b6e63a6";
            driver.FindElement(By.Id("SearchParameter")).SendKeys(pnr);
            driver.FindElement(By.Id("patientSearchBtn")).Submit();

            // Lese patientInRegistryGuid fra URL
            var PageLocation = driver.Url.Split('/');
            var patientInRegistryGuid = PageLocation[PageLocation.Length - 1];
            // Nytt skjema
            //driver.FindElement(By.ClassName("mqr-create-form")).Click();
            //var datetime = driver.FindElement(By.LinkText("Opprett"));
            //datetime.SendKeys("01.01.2017");
            //driver.FindElement(By.ClassName("save")).Submit();
            var formDate = "01.01.2017";
            var url = appUrl + "MainForm/Create?patientInRegistryGuid=" + patientInRegistryGuid + "&formDate=" + formDate;
            //driver.Url = url;
            driver.Navigate().GoToUrl(url);

            // Lese ny Skjema GUID fra URL
            var PageURL = driver.Url.Split('/');
            var s = PageURL[PageURL.Length - 1];
            var skjemaGUID = s.Substring(0, s.IndexOf("?"));

            // Tilbake til Pasientsiden
            driver.FindElement(By.CssSelector("#navbar .btn a")).Click();

            // Finn det nye skjemaer vha skjema GUID
            //            var trSkjema = $"//tr[@data-formid='{skjemaGUID}']";
            //            var tr = _chromeDriver.FindElement(By.XPath(trSkjema));
            var cssSelector = $"tr[data-formid='{skjemaGUID}'] button.form-delete";
            Console.WriteLine("cssSelector : " + cssSelector);
            var button = driver.FindElement(By.CssSelector(cssSelector));

            // Slette skjema
            //button.Click();

            //var cssConfirm = "button[onclick='deleteFormConfirmed();']";
            //var confirm = driver.FindElement(By.CssSelector(cssConfirm));
//            confirm.Click();

            //driver.ExecuteJavaScript("arguments[0].click()", new[] {confirm});

        }

        private IWebDriver CreateIEDriver()
        {
            var ieOptions = new InternetExplorerOptions
            {
                EnsureCleanSession = true
            };
            return new InternetExplorerDriver(ieOptions);
        }
        private IWebDriver CreateEdgeDriver()
        {
            // location for MicrosoftWebDriver.exe
            var serverPath = "Microsoft Web Driver";
            if (System.Environment.Is64BitOperatingSystem)
            {
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), serverPath);
            }
            else
            {
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles%"), serverPath);
            }

            var options = new EdgeOptions
            {
                PageLoadStrategy = EdgePageLoadStrategy.Default
            };
            
            Console.WriteLine(serverPath);
            return new EdgeDriver(serverPath);
        }
        private IWebDriver CreatePhantomJsDriver()
        {
            var options = new PhantomJSOptions();
            {                
            };
            return new PhantomJSDriver(options);
        }

        private IWebDriver CreateChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("-incognito");
            return new ChromeDriver(chromeOptions);
        }

        private void SignIn(IWebDriver driver)
        {
            //Launch the Website
            driver.Url = appUrl;

            // Pålogging STS
            var username = driver.FindElement(By.Id("UserName"));
            username.Clear();
            username.SendKeys("promstest");
            var password = driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("promstest");
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            var loggIn = driver.FindElement(By.Id("singlebutton"));
            loggIn.Click();

            // Avdeling
            var resh = driver.FindElement(By.Id("ReshId"));
            var selectElement = new SelectElement(resh);
            selectElement.SelectByValue("102212");
            driver.FindElement(By.Id("singlebutton")).Click();
        }

        private void DivTest()
        {
            IWebDriver _driver = CreateChromeDriver();

            // Storing Title name in String variable
            String Title = _driver.Title;

            // Storing Title length in Int variable
            int TitleLength = _driver.Title.Length;

            // Printing Title name on Console
            Console.WriteLine("Title of the page is : " + Title);

            // Printing Title length on console
            Console.WriteLine("Length of the Title is : " + TitleLength);

            // Storing URL in String variable
            String PageURL = _driver.Url;

            // Storing URL length in Int variable
            int URLLength = PageURL.Length;

            // Printing URL on Console
            Console.WriteLine("URL of the page is : " + PageURL);

            // Printing URL length on console
            Console.WriteLine("Length of the URL is : " + URLLength);

            // Storing Page Source in String variable
            String PageSource = _driver.PageSource;

            // Storing Page Source length in Int variable
            int PageSourceLength = _driver.PageSource.Length;

            // Printing Page Source on console
            Console.WriteLine("Page Source of the page is : " + PageSource);

            // Printing Page SOurce length on console
            Console.WriteLine("Length of the Page Source is : " + PageSourceLength);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        private void TearDown(IWebDriver driver)
        {
            driver.Close();
            //Closing browser
            driver.Quit();
        }

        private void SignOut(IWebDriver driver)
        {
            var signOut = driver.FindElement(By.CssSelector("#hemitTop .userinfo a"));
            signOut.Click();
        }
    }
}
