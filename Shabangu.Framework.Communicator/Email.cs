using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Shabangu.Communicator
{
    public class EmailMessage
    {
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<EmailAttachedment> Attachments { get; set; }
    }
    public class EmailSettings
    {
        public string SMTP_ServerName { get; set; }
        public int SMTP_Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsHtmlMessage { get; set; }
    }
    public class EmailAttachedment
    {
        public string Filename { get; set; }
        public byte[] FileContent { get; set; }
    }
}
