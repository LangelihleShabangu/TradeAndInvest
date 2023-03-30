using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Shabangu.Communicator
{
    public class EmailLogic
    {
        private int _port;
        private string _server;
        private string _username;
        private string _password;
        private bool _IsHtml;
        public EmailLogic(EmailSettings settings)
        {
            _port = settings.SMTP_Port;
            _server = settings.SMTP_ServerName;
            _password = settings.Password;
            _username = settings.Username;
            _IsHtml = settings.IsHtmlMessage;
        }
        public bool SendEmail(EmailMessage message)
        {
            try
            {
                foreach (var address in message.Recipients)
                {
                    MailMessage Message = new MailMessage(_username, address);
                    Message.Body = message.Body;
                    Message.Subject = message.Subject;
                    Message.IsBodyHtml = _IsHtml;

                    if (message != null && message.Attachments != null)
                    {
                        foreach (var file in message.Attachments)
                        {
                            Attachment att = new Attachment(new MemoryStream(file.FileContent), file.Filename);
                            Message.Attachments.Add(att);
                        }
                    }

                    SmtpClient client = new SmtpClient(_server, _port);
                    client.Credentials = new NetworkCredential(_username, _password);
                    client.Send(Message);

                }
                return true;
            }
            catch(Exception ex)
            {
                string exception = ex.ToString();
               throw;
            }
        }
    }
}
