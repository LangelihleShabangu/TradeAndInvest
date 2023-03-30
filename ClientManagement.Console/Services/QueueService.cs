
using ClientManagement.Console.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientManagement.Console.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration config;

        public QueueService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendMessage<T>(T serviceMessageBus, string queueName)
        {
            var queueClient = new QueueClient(config.GetConnectionString("AzureServiceBus"), queueName);
            string messageBody = JsonSerializer.Serialize(serviceMessageBus);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.SendAsync(message);
        }
    }
}
