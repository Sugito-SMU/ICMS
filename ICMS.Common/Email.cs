﻿using System;
using System.Configuration;
using System.Net.Mail;

namespace ICMS.Common
{
    public class Email
    {

        public static void SendEmail(string to, string cc, string bcc, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["EmailFrom"];

            MailMessage mailMessage = new MailMessage();

            if (!string.IsNullOrWhiteSpace(fromAddress))
            {
                mailMessage.From = new MailAddress(fromAddress);
            }

            if (!string.IsNullOrWhiteSpace(to))
            {
                foreach (string s in to.Trim().Split(','))
                {
                    mailMessage.To.Add(new MailAddress(s.Trim()));
                }
            }

            if (!string.IsNullOrWhiteSpace(cc))
            {
                foreach (string s in cc.Trim().Split(','))
                {
                    mailMessage.CC.Add(new MailAddress(s.Trim()));
                }
            }

            if (!string.IsNullOrWhiteSpace(bcc))
            {
                foreach (string s in bcc.Trim().Split(','))
                {
                    mailMessage.Bcc.Add(new MailAddress(s.Trim()));
                }
            }

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = body;

            mailMessage.Subject = subject;

            SmtpClient smtp = new SmtpClient();

            int maxTry = 5;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    smtp.Send(mailMessage);
                    i = maxTry;
                }
                catch (Exception ex)
                {
                    if (i == maxTry - 1)
                        throw ex;
                    System.Threading.Thread.Sleep(500);
                }
            }
            mailMessage.Dispose();
        }

        public static void SendEmailForError(Exception ex, string url, string userId)
        {
            string to = ConfigurationManager.AppSettings["ErrorMsgTo"];
            string subject = ConfigurationManager.AppSettings["ErrorMsgSubject"];
            string body = string.Format(@" 
                  <html> 
                  <body> 
                  <table cellpadding=""5"" cellspacing=""0"" border=""1""> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">URL:</td> 
                  <td>{0}</td> 
                  </tr> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">User:</td> 
                  <td>{1}</td> 
                  </tr> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">Message:</td> 
                  <td>{2}</td> 
                  </tr> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">Details:</td> 
                  <td>{3}</td> 
                  </tr>  
                  </table> 
                </body> 
                </html>",
                url,
                userId,
                ex.Message,
                ex.ToString().Replace(Environment.NewLine, "<br />"));
            SendEmail(to, null, null, subject, body);
        }

        public static void SendEmailForError(string exMsg, string url, string userId)
        {
            string to = ConfigurationManager.AppSettings["ErrorMsgTo"];
            string subject = ConfigurationManager.AppSettings["ErrorMsgSubject"];
            string body = string.Format(@" 
                  <html> 
                  <body> 
                  <table cellpadding=""5"" cellspacing=""0"" border=""1""> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">URL:</td> 
                  <td>{0}</td> 
                  </tr> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">User:</td> 
                  <td>{1}</td> 
                  </tr> 
                  <tr> 
                  <tdtext-align: right;font-weight: bold"">Details:</td> 
                  <td>{2}</td> 
                  </tr>  
                  </table> 
                </body> 
                </html>",
                url,
                userId,
                exMsg.ToString().Replace(Environment.NewLine, "<br />"));
            SendEmail(to, null, null, subject, body);
        }

    }
}
