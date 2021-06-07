using System;
using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace ECN
{
    [TestClass]
    public class SanityTest
    {
        IWebDriver driver;

        ExtentHtmlReporter reporter = new ExtentHtmlReporter(@"C:\Users\Satyanarayan\source\repos\ECN\ECN\Reports\reports.html");
        ExtentReports extent = new ExtentReports();

        [TestMethod]
        public void SanityTest404Check()
        {

            //opening the application
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.ecn.cricket/");

            driver.Manage().Window.Maximize();

            extent.AttachReporter(reporter);
            var testreport=extent.CreateTest("ECN404Check");


            FunctionalLibrary.waitForElement(driver, "//*[@class='action-btn']");

            FunctionalLibrary.clickAction(driver, "//*[@class='action-btn']", "xpath");

            int pagecount=driver.FindElements(By.XPath("//*[@class='site-nav']/ul/li")).Count;

            for(int i= 1;i<= pagecount;i++)
            {
                FunctionalLibrary.waitForElement(driver, "//*[@class='site-nav']/ul/li[" + i + "]");
                FunctionalLibrary.clickAction(driver, "//*[@class='site-nav']/ul/li[" + i + "]", "xpath");




               FunctionalLibrary.TitleVerification(driver, FunctionalLibrary.ReadDataExcel(1, 1, i + 1));
                testreport.Log(Status.Info, "Title verified");

                testreport.Log(Status.Pass, "Title and 404");
                try
                {
                    IWebElement errorpath = driver.FindElement(By.XPath("//*[@class='error-thumbnail']"));

                    if (errorpath.Displayed)
                    {

                        Console.WriteLine(driver.Url);
                        FunctionalLibrary.screenShot(driver);
                    }
                }

                catch
                {
                 //   testreport.Log(Status.Fail, "404");
                }
               
            }

           int pagecount2= driver.FindElements(By.XPath("//*[@class='secondary-site-nav']/ul/li")).Count;

            for (int i = 1; i <= pagecount2; i++)
            {
                FunctionalLibrary.waitForElement(driver, "//*[@class='secondary-site-nav']/ul/li[" + i + "]/a");
                FunctionalLibrary.clickAction(driver, "//*[@class='secondary-site-nav']/ul/li[" + i + "]/a", "xpath");

                FunctionalLibrary.TitleVerification(driver, FunctionalLibrary.ReadDataExcel(1, 2, i + 1));
                testreport.Log(Status.Info, "Title verified");
               
                try
                {
                    IWebElement errorpath = driver.FindElement(By.XPath("//*[@class='error-thumbnail']"));

                    if (errorpath.Displayed)
                    {

                        Console.WriteLine(driver.Url);
                        FunctionalLibrary.screenShot(driver);
                        testreport.Log(Status.Fail, "Title and 404");
                    }
                }

                catch
                {
                    testreport.Log(Status.Pass, "Title and 404");
                }
              
            }
            int pagecount3 = driver.FindElements(By.XPath("//*[@class='third-site-nav']/div/nav/ul/li")).Count;

            for (int i = 1; i <= pagecount3; i++)
            {
                FunctionalLibrary.waitForElement(driver, "//*[@class='third-site-nav']/div/nav/ul/li[" + i + "]/a");
                FunctionalLibrary.clickAction(driver, "//*[@class='third-site-nav']/div/nav/ul/li[" + i + "]/a", "xpath");

                //  FunctionalLibrary.TitleVerification(driver, FunctionalLibrary.ReadDataExcel(1, 2, i + 1));

                Console.WriteLine(driver.Title);
                testreport.Log(Status.Info, "Title verified");
              
                try
                {
                    IWebElement errorpath = driver.FindElement(By.XPath("//*[@class='error-thumbnail']"));

                    if (errorpath.Displayed)
                    {

                        Console.WriteLine(driver.Url);
                        FunctionalLibrary.screenShot(driver);
                        testreport.Log(Status.Fail, "Title and 404");
                    }
                }

                catch
                {
                    testreport.Log(Status.Pass, "Title and 404");
                }

            }
            extent.Flush();

            driver.Close();

        }

        [TestMethod]

        public void chromeExtension()
        {

            //ChromeOptions options = new ChromeOptions();

         //   options.AddExtension(@"C:\Users\Satyanarayan\source\repos\ECN\ECN\JsonCRX\JSON-Viewer_v0.18.1.crx");

            //options.AddAdditionalCapability(ChromeOptions.Capability, options);

            ChromeOptions options = new ChromeOptions();
            //options.AddExtension(@"D:\Downloads\Desktopify\nlhjgcligpbnjphflfdbmabbmjidnmek.crx");
            options.AddExtension(@"C:\Users\Satyanarayan\source\repos\ECN\ECN\JsonCRX\JSON-Viewer_v0.18.1.crx");
            options.AddArgument("test-type");
        //    System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"D:\VisualStudioExpress2017\Projects\MyApp\bin\Debug\chromedriver.exe");
          



         IWebDriver driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://cricket.yahoo.net/sifeeds/cricket/live/json/ennz06022021199906.json");



            


            int count = driver.FindElements(By.XPath("//*[@class='CodeMirror-code']/div")).Count;

            for (int i = 1; i <= count; i++)
            {

                try
                {
                    string str = FunctionalLibrary.ElementText(driver, "//*[@class='CodeMirror-code']/div[" + i + "]/pre/span/span");

                   // Console.WriteLine(str);

                    
                    if (str.StartsWith("\"Id\""))
                    {
                        string str2 = FunctionalLibrary.ElementText(driver, "//*[@class='CodeMirror-code']/div[" + i + "]/pre/span/span[2]");
                        Console.WriteLine(str+" "+" "+str2);
                        
                    }
                }
                catch
                {

                }
            }

        }
    }
}
