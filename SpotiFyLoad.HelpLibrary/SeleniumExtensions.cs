using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpotiFyLoad.HelpLibrary
{
    class SeleniumExtensions
    {

        public static bool GetSpotiFyInChrome(string spotifyUrl, string login, string password)
        {

            bool chromeLoaded = false;
            try
            {
                #region ChromeDriver
                using (var driver = new ChromeDriver())
                {


                    // Go to the home page
                    driver.Navigate().GoToUrl(spotifyUrl);

                    Console.WriteLine("Loaded Page : "+ driver.Url + driver.Title);

                    var elementExists = driver.FindElements(By.CssSelector("a[id='nav-link-log out']")).Any();

                    if (elementExists)
                    {
                        var logOutBtns = driver.FindElement(By.CssSelector("a[id='nav-link-log out']"));
                        logOutBtns.SendKeys(Keys.Enter);
                    }


                    WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 1, 0));
                    var btn =  wait.Until(d => d.FindElement(By.Id("header-login-link")));

                    //var btn = driver.FindElement(By.Id("header-login-link"));

                   #region If btn
                    //if (btn.Displayed)
                    //{
                        btn.SendKeys(Keys.Enter);

                        var userNameField = driver.FindElementById("login-username");
                        var userPasswordField = driver.FindElementById("login-password");
                        var loginRemember = driver.FindElementById("login-remember");
                        var loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Log In')]"));

                        userNameField.SendKeys(login);
                        userPasswordField.SendKeys(password);

                        if (loginRemember.Selected)
                        {
                            //loginRemember.Click();
                            driver.FindElementByClassName("control-indicator").Click();
                        }
                        loginButton.Click();

                    WebDriverWait waits = new WebDriverWait(driver, new TimeSpan(0, 1, 0));

                    //var webPlayerLink = waits.Until(d => d.FindElement(By.Id("nav-link-play")));

                    var webPlayerLink = waits.Until(ExpectedConditions.ElementExists(By.Id("nav-link-play")));
                    webPlayerLink.Click();

                    //var tryAgain = true;

                    //while (tryAgain)
                    //{
                    //    Console.WriteLine(driver.Title);
                    //    var webPlayerLink = driver.FindElementById("nav-link-play");

                    //    if (webPlayerLink.Displayed)
                    //    {

                    //        webPlayerLink.Click();
                    //        tryAgain = false;

                    //    }
                    //    else
                    //    {
                    //        Thread.Sleep(20000);
                    //        driver.Keyboard.SendKeys(Keys.Shift);

                    //        driver.Navigate().GoToUrl(spotifyUrl + "us/redirect/webplayerlink");
                    //    }


                    //}
                    Thread.Sleep(20000);

                    driver.Quit();

                    #endregion


                }
                #endregion
                chromeLoaded = true;

            }
            catch (Exception ex)
            {
                string from = ConfigurationManager.AppSettings["fromMailAccount"];
                string to = ConfigurationManager.AppSettings["toMailAccount"];
                string pass = ConfigurationManager.AppSettings["mailPassword"];
                string subject = "SpotyFy Renew Process is fail";
                string body = ex.Message;
                bool mail = Common.SendGMail(from, to, pass, subject, body);
                SpotyFyLogger log = new SpotyFyLogger();
                log.LogWriter(body);
                throw new Exception(ex.Message, ex);
            }

            return chromeLoaded;
        }
    }

}

