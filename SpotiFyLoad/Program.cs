using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SpotiFyLoad.HelpLibrary;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Xml.Linq;
using System.Threading;

namespace SpotiFyLoad
{
    class Program
    {


        static void Main(string[] args)
        {
            string from = ConfigurationManager.AppSettings["fromMailAccount"];
            string to = ConfigurationManager.AppSettings["toMailAccount"];
            string pass = ConfigurationManager.AppSettings["mailPassword"];

            string mailSubject =string.Empty;
            string mailBody = string.Empty;
            string processCommonStatus = string.Empty;



            StringBuilder smallLog = new StringBuilder();// = string.Empty;
            string date = DateTime.Today.ToShortDateString();
            SpotyFyLogger log = new SpotyFyLogger();

            string spotifyUrl = ConfigurationManager.AppSettings["spotifyUrl"];

            string[,] userTable = {
                       { "zabeling", "ge12an12" },
                       { "gera19999", "ge12an12" }
                   };

            try
            {

                for (int i = 0; i < userTable.GetLength(0); i++)
                {
                    Console.WriteLine(userTable[i, 0] + " " + userTable[i, 1]);
                    string spotifyUser = userTable[i, 0];
                    string spotifyPass = userTable[i, 1];


                    smallLog.Append(date + " Process Sync User " + spotifyUser + " Is Started ");

                    bool getUrlInChrome = SpotiFyLoad.HelpLibrary.Common.GetSpotiFyInChrome(spotifyUrl, spotifyUser, spotifyPass);

                    if (getUrlInChrome) { smallLog.AppendLine("and Done"); }
                    else { smallLog.AppendLine(" and Fail"); }

                    processCommonStatus = "Done";


                    Console.Write(smallLog.ToString());
                    Console.ReadKey();

                }


            }
            catch (Exception ex)
            {

                processCommonStatus = "Failed";
                mailSubject = "SpotyFy Renew Account Process " + processCommonStatus;
                mailBody = "Process failed with reason : " +ex.Message;
                bool mail = HelpLibrary.Common.SendGMail(from, to, pass, mailSubject, mailBody);
                throw new Exception(ex.Message, ex);

            }
            finally
            {
                string subject = "SpotyFy Renew Account Process was " + processCommonStatus;
                string body = smallLog.ToString();
                bool mail = HelpLibrary.Common.SendGMail(from, to, pass, subject, body);
                if (mail)
                {
                    smallLog.AppendLine("email to: '" + to + "' sended");
                }
                
                log.LogWriter(smallLog.ToString());
                smallLog.Clear();

            }




        }

       
    }
}