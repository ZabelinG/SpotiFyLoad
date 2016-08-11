using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotiFyLoad.HelpLibrary
{
    public class Common
    {

        public static bool GetSpotiFyInChrome(string url,string login, string pass)
        {

            bool inChromeLoaded;
            try
            {
                inChromeLoaded = SeleniumExtensions.GetSpotiFyInChrome(url,login,pass);

            }
            catch (Exception ex)
            {
                inChromeLoaded = false;
                throw new Exception(ex.Message, ex);

            }
            return inChromeLoaded;
        }


        public static bool SendGMail(string from, string to, string password, string subject, string body)
        {

            bool mailSended;
            try
            {
                mailSended = SendMail.SendGMail(from, to, password, subject, body);
                
            }
            catch (Exception ex)
            {
                mailSended = false;
                throw new Exception(ex.Message, ex);

            }
            return mailSended;
        }
	   

    }
}
