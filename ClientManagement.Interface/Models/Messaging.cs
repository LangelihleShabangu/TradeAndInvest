using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.BusinessLogic
{
    public class Messaging
    {
        public void SendSMS(string CellNumber, string Firstname, string Surname, string OTP)
        {
            try
            {

                string text = string.Empty;
                var username = ConfigurationManager.AppSettings["SMSUserName"];
                var password = ConfigurationManager.AppSettings["SMSPassword"];
                var Messageval = ConfigurationManager.AppSettings["MessageOne"];
                string url = ConfigurationManager.AppSettings["SMSGateWay"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Dear {0} {1}, Your OTP number is {2}.", Firstname, Surname, OTP));
                
                var message = new Shabangu.Types.Communicator.TextMessage() { Cellnumber = CellNumber.Trim(), Message = string.IsNullOrEmpty(sb.ToString()) ? Messageval.ToString() : sb.ToString(), Username = username, Password = password, BaseUrl = url, };
                new Shabangu.Communicator.TextMessageLogic().SendTextMessage(message);
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }

        public void SendDocumentSMS(string CellNumber, string Firstname, string Surname, string Doc)
        {
            try
            {
                string text = string.Empty;
                var username = ConfigurationManager.AppSettings["SMSUserName"];
                var password = ConfigurationManager.AppSettings["SMSPassword"];
                var Messageval = ConfigurationManager.AppSettings["MessageOne"];
                string url = ConfigurationManager.AppSettings["SMSGateWay"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Dear {0} {1}, we acknowledge the receipt of your registration into the COJ/DED Informal Trading portal, however we are still awaiting supporting documentation like {2}.", Firstname, Surname, Doc));

                var message = new Shabangu.Types.Communicator.TextMessage() { Cellnumber = CellNumber.Trim(), Message = string.IsNullOrEmpty(sb.ToString()) ? Messageval.ToString() : sb.ToString(), Username = username, Password = password, BaseUrl = url, };
                new Shabangu.Communicator.TextMessageLogic().SendTextMessage(message);
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }
        
        public void SendRegistrationSMS(string CellNumber, string Firstname, string Surname)
        {
            try
            {
                string text = string.Empty;
                var username = ConfigurationManager.AppSettings["SMSUserName"];
                var password = ConfigurationManager.AppSettings["SMSPassword"];
                var Messageval = ConfigurationManager.AppSettings["MessageOne"];
                string url = ConfigurationManager.AppSettings["SMSGateWay"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Dear {0} {1}, we acknowledge the receipt of your registration into COJ/DED Informal Trader. This is to confirm that your registration has been successful.", Firstname, Surname));

                var message = new Shabangu.Types.Communicator.TextMessage() { Cellnumber = CellNumber.Trim(), Message = string.IsNullOrEmpty(sb.ToString()) ? Messageval.ToString() : sb.ToString(), Username = username, Password = password, BaseUrl = url,  };
                new Shabangu.Communicator.TextMessageLogic().SendTextMessage(message);
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }

        public void SendSMS(string CellNumber, string Name, decimal Amount, string ReferenceNumber, string prods)
        {
            try
            {
                string text = string.Empty;
                var username = ConfigurationManager.AppSettings["SmsUsename"];
                var password = ConfigurationManager.AppSettings["SmsPassword"];
                var Messageval = ConfigurationManager.AppSettings["MessageOne"];

                var Bank = ConfigurationManager.AppSettings["Bank"];
                var Account = ConfigurationManager.AppSettings["Account"];
                var Type = ConfigurationManager.AppSettings["Type"];
                var Branch = ConfigurationManager.AppSettings["Branch"];

                var Amounts = Amount.ToString("c", ClientManagement.DomainModel.Configuration.CultureInfoSettings.GetZACulture().NumberFormat);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Dear {0},", Name));
                sb.AppendLine(string.Format("Purchase successfully registered. Please pay {0} to the following account: ", Amounts));
                sb.AppendLine(string.Format("Account Holder : {0} ", ConfigurationManager.AppSettings["AccountHolder"].ToString()));
                sb.AppendLine(string.Format("Bank : {0} ", ConfigurationManager.AppSettings["Bank"].ToString()));
                sb.AppendLine(string.Format("Account No : {0} ", ConfigurationManager.AppSettings["Account"].ToString()));
                sb.AppendLine(string.Format("Branch : {0} ", ConfigurationManager.AppSettings["Branch"].ToString()));
                sb.AppendLine(string.Format("Type of Account : {0} ", ConfigurationManager.AppSettings["Type"].ToString()));
                sb.AppendLine(string.Format("Reference No : {0}", ReferenceNumber));
                sb.AppendLine(string.Format("Products are : {0}", prods));

                var message = new Shabangu.Types.Communicator.TextMessage() { Cellnumber = CellNumber.Trim(), Message = string.IsNullOrEmpty(sb.ToString()) ? Messageval.ToString() : sb.ToString(), Username = username, Password = password };
                new Shabangu.Communicator.TextMessageLogic().SendTextMessage(message);
                SendSingleMail(ReferenceNumber);
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }

        public void SendSingleMail(string ReferenceNumber)
        {
            Shabangu.Communicator.EmailMessage msg = new Shabangu.Communicator.EmailMessage();

            msg.Recipients = new List<string>() { System.Configuration.ConfigurationManager.AppSettings["NotificationEmail"].ToString() };
            msg.Subject = string.Format("New Order {0} ", ReferenceNumber);

            msg.Body = string.Format("Good day<br/><br/>" +
                "An order has been please today please follow up accordingly on the admin Application by using ref number.<br/><br/>" +
                "For any further enquiries, kindly contact your us.<br/><br/>" +
                "Regards AGH,<br/>");

            var username = System.Configuration.ConfigurationManager.AppSettings["Usename"].ToString();
            var password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

            var port = System.Configuration.ConfigurationManager.AppSettings["Port"];
            var server = System.Configuration.ConfigurationManager.AppSettings["Host"].ToString();

            var settings = new Shabangu.Communicator.EmailSettings()
            {
                IsHtmlMessage = true,
                Password = password,
                SMTP_Port = Convert.ToInt32(port),
                SMTP_ServerName = server,
                Username = username
            };
            new Shabangu.Communicator.EmailLogic(settings).SendEmail(msg);
        }


        public void SendSMS(string CellNumber)
        {
            try
            {
                string text = string.Empty;
                var username = ConfigurationManager.AppSettings["SmsUsename"];
                var password = ConfigurationManager.AppSettings["SmsPassword"];

                var Messageval = ConfigurationManager.AppSettings["MessageOne"];

                StringBuilder sb = new StringBuilder();
                //sb.AppendLine(string.Format("Hi {0}", Name));
                //sb.AppendLine(string.Format("Successfully Registered please deposit {0} to Account", Amount));
                //sb.AppendLine(string.Format("Bank : ABSA"));
                //sb.AppendLine(string.Format("Account No : 4090631661"));
                //sb.AppendLine(string.Format("Type of Account : Cheque Account"));
                //sb.AppendLine(string.Format("Reference Number : {0}", IdNumber));

                sb.AppendLine(string.Format("Reference : Your Id Number"));

                var message = new Shabangu.Types.Communicator.TextMessage() { Cellnumber = CellNumber.Trim(), Message = string.IsNullOrEmpty(sb.ToString()) ? Messageval.ToString() : sb.ToString(), Username = username, Password = password };
                new Shabangu.Communicator.TextMessageLogic().SendTextMessage(message);
            }
            catch (Exception ex)
            {
                string exc = ex.ToString();
            }
        }
    }
}