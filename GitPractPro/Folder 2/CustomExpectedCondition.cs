using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPractPro.Folder_2
{
    class CustomExpectedCondition:Class1
    {

        // wowl
        public static Func<IWebDriver,bool> WaitFor(By by)

        {
            Func<IWebDriver, bool> myCustomCondition;


            myCustomCondition = driver => 
            { 
            IWebElement element = Class1.objDriver.FindElement(by);
                if(element!=null)
                {
                    return true;
                }

            }

        }
    }
}
