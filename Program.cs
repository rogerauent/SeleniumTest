using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Selenium
{
    //[TestFixture]
    public class Program
    {
        //[Test]
        public static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--ignore-certificate-errors");
            //chromeOptions.AddArgument("--disable-web-security");
            chromeOptions.AddArgument("-incognito");
            //IWebDriver driver = new ChromeDriver(chromeOptions);

            driver.Url = "http://www.demoqa.com";
            //Console.WriteLine("her");
            driver.Quit();

        }
    }
}
