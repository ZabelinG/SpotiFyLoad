using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SpotiFyLoad.HelpLibrary
{
    class SendMail
    {

        internal static bool SendGMail(string from, string to, string password, string subject, string body)
        {

            var fromAddress = new MailAddress(from, "From Name");
            var toAddress  = new MailAddress(to, "To Name");


            //string fromPassword = password;
            //const string subject = "SpotiFyLoad error";
            //const string body = "SpotiFyLoad : error in renwe process ";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return true;

        }





       
    }
}
