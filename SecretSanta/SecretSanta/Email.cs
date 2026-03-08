using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    public class Email
    {
        /// <summary>
        /// Opens a blank email in Outlook addressed to the recipient
        /// </summary>
        /// <param name="recipient"></param>
        public static void NewEmail(string recipient)
        {
            Process.Start("mailto:" + recipient);
        }
        /// <summary>
        /// Opens a blank email in Outlook addressed to the recipient list
        /// </summary>
        /// <param name="recipientList"></param>
        public static void NewEmail(List<string> recipientList)
        {
            string recipients = "";

            foreach (string name in recipientList)
            {
                recipients += name + ";";
            }

            if (recipients.Count() > 2043)
            {
                //MessageBox.Show("Recipent list too long for mailto", "Error!");
                return;
            }

            Process.Start("mailto:" + recipients);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool SendEmail(string from, string to, string subject, string body)
        {
            SmtpClient email = new SmtpClient();

            //MailMessage message = new MailMessage(from, to, subject, body);
            MailMessage message = new MailMessage(from, "ofey@outlook.com", subject, body);
            message.IsBodyHtml = true;
            email.Host = "mail.southwark.anglican.org";

            try
            {
                email.Send(message);
            }
            catch (SmtpException)
            {
                return false;
            }

            return true;
        }
    }
}
