using System;
using System.Linq;
using System.Collections.Generic;

using log4net;

namespace EmailPlug
{
    internal static class EmailNotification
    {
        internal static void Notify(string message, List<string> recipients, Config config)
        {
            List<string> recipientEmails = CleanRecipientList(recipients);

            if (recipientEmails.Count == 0)
                return;

            EmailSender.Send("[MergeBot] Notify message", message, recipientEmails, config);
        }

        static List<string> CleanRecipientList(List<string> recipients)
        {
            List<string> result = new List<string>();

            foreach (string recipient in recipients)
            {
                if (!IsValidEmail(recipient))
                    continue;

                result.Add(recipient);
            }

            return result.Distinct().ToList();
        }

        static bool IsValidEmail(string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
                return false;

            try
            {
                new System.Net.Mail.MailAddress(recipient);
                return true;
            }
            catch (FormatException)
            {
                mLog.ErrorFormat("'{0}' is not a valid mail address");
                return false;
            }
        }

        static readonly ILog mLog = LogManager.GetLogger("emailplug");
    }
}
