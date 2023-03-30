
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientManagement.ServiceDetails.Services
{
    public class QueueService : IQueueService
    {        
        public void SendMessage<T>(T serviceMessageBus, string queueName)
        {
            try
            {
                var queueClient = new QueueClient(System.Configuration.ConfigurationManager.AppSettings["AzureServiceBus"].ToString(), queueName);
                string messageBody = JsonSerializer.Serialize(serviceMessageBus);

                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                queueClient.SendAsync(message);
            }
            catch(Exception EX)
            {
                if (!string.IsNullOrEmpty(EX.ToString()))
                {
                    string x = EX.Message.ToString();
                }
            }
        }
    }
}
