using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace WebTester
{
    public class AutomatedProcessModel

    {
        private static IWebDriver WebDriver { get; set; } = new ChromeDriver(@"C:\Users\Colin\Desktop\C#\");

        public static void GoGoScript()
        {

            NavigateToHomeScreen();
            var mealList = WebDriver.FindElements(By.Id("meal"));

            for (int i = 0; i < mealList.Count; i++)
            {
                var windowUrl = WebDriver.Url;
                var meals = WebDriver.FindElements(By.Id("meal"));
                ClickOnElement(meals[i]);
                Thread.Sleep(3000);
                if (WebDriver.Url != windowUrl)
                {
                    DownloadTheMeal();                    
                }
                else
                {
                    ClickOnElement(meals[i]);
                    Thread.Sleep(3000);
                    DownloadTheMeal();
                    
                }  
            }
        }


        private static void DownloadTheMeal()
        {
            var printerButton = WebDriver.FindElement(By.XPath(@"//*[@id=""mainContent""]/article/section[2]/div[2]/ul/li[5]/a/div"));
            if (printerButton.Enabled)
            {
                printerButton.Click();
                Thread.Sleep(500);
                SendKeys.SendWait("^(s)");
                Thread.Sleep(1000);
                SendKeys.Send("{ENTER}");
                Thread.Sleep(1000);
                SendKeys.SendWait("^(w)");
                NavigateToHomeScreen();
                
            }
            Thread.Sleep(500);          
            
        }
        
        private static void NavigateToHomeScreen()
        {
            WebDriver.Navigate().GoToUrl("https://www.homechef.com/");

            var menu = WebDriver.FindElement(By.XPath("/ html / body / header / div / nav / div[4] / div[1] / ul / li[1] / a"));
            if (menu.Displayed)
            {
                menu.Click();
            }
            Thread.Sleep(2000);

        }

        private static void ClickOnElement(IWebElement element)
        {
            Actions actions = new Actions(WebDriver);
            actions.MoveToElement(element).Click().Build().Perform();
        }

        public static void GetUrlsForAll()
        {
            NavigateToHomeScreen();

        }

       

    }
}
