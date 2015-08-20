using System;
using System.Net.Mail;
using Hackaton.Boilerplate.Abstraction.Internals;
using System.Diagnostics;

namespace Hackaton.Boilerplate.Shared.Internals
{
    public class MailManager : IMailManager
    {
        public void Send(string from, string[] to, string subject, string body, Attachment attach)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var message = new MailMessage();
                    message.Attachments.Add(attach);
                    message.Subject = subject;
                    message.Body = body;
                    foreach (var address in to)
                    {
                        message.To.Add(address);
                    }

                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                // if this fails, nobody will help us
                Debug.WriteLine(ex);
            }
        }

        public void Send(string from, string to, string subject, string body, Attachment attach)
        {
            Send(from, to, subject, body, attach);
        }
    }
}
