using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPractPro
{
    public class Class1
    {
      public static  IWebDriver objDriver;
        [Test]
        public  void m1()
        {
            // hellowads
            //added extra comments
            //added trhible comment
            // added  4 ht comment
            //fifth
            // sync testfasdfasd
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService chrService = ChromeDriverService.CreateDefaultService("C://Drivers//");
             objDriver = new ChromeDriver(chrService, options);
            objDriver.Manage().Window.Maximize();
            // objDriver.Navigate().GoToUrl("http://uitestpractice.com/Students/Contact");
            objDriver.Url = "http://uitestpractice.com/Students/Contact";
            
          

            objDriver.FindElement(By.LinkText("This is a Ajax link")).Click();
        }
    }
}
