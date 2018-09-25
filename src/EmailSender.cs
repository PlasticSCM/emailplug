using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

using log4net;

namespace EmailPlug
{
    internal static class EmailSender
    {
        internal static void Send(
            string subject, string body, List<string> recipients, Config config)
        {
            mLog.DebugFormat("Sending email with subject {0} from {1} to {2}",
                subject, config.SenderEmail, GetFormattedRecipients(recipients));

            using (MailMessage message = BuildMailMessage(
                config.SenderEmail, subject, body, recipients))
            {
                SendMessage(
                    config.SmtpServer,
                    config.SmtpPort,
                    config.SenderEmail,
                    config.SenderPassword,
                    message);
            }

            mLog.DebugFormat("Email sent successfully");
        }

        static void SendMessage(
            string smtpServer, int smtpPort,
            string sender, string password, MailMessage message)
        {
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(sender, password);
                smtpClient.Send(message);
            }
        }

        static MailMessage BuildMailMessage(
            string sender, string subject, string body, List<string> recipients)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(sender);
            message.Subject = subject;
            message.Body = body;

            foreach (string destination in recipients)
                message.To.Add(destination);

            return message;
        }

        static string GetFormattedRecipients(List<string> recipients)
        {
            return string.Join(",", recipients.ToArray());
        }

        static readonly ILog mLog = LogManager.GetLogger("emailplug");
    }
}
