using Microsoft.Extensions.Configuration;
using System;

namespace ClientManagement.Service
{  
    public class SyncDataInformation
        {
            static string connectionString = "Endpoint=sb://ded-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=qyN5BFDUavpmOHraD9IskgcXNRkbOcyGakMMOCxRPjM=";
            static string queueName = "account-queue";
            private static IConfiguration config;
            public static async Task SendAccountMessage()
            {
                var account = new
                {
                    rif_dedaccountid = "",
                    name = "Company Name",
                    address1_line1 = "",
                    address1_line2 = "",
                    address1_line3 = "",
                    address1_city = "",
                    address1_stateorprovince = "",
                    address1_postalcode = "",
                    address1_country = "",
                    rif_businesscategory = 908510000,

                    rif_womanowendbusiness = true,
                    industrycode = 16,
                    rif_planningtoexport = true,
                    rif_joburgbasedcompany = true,
                    rif_companydocument = "www.google.com",
                    rif_exporterregion = "Africa",
                    rif_exportercountry = "Tanzania",

                    rif_noofyearsasanexporter = 24,
                    rif_noofemployees = 23,
                    rif_annualturnover = 455.00,
                    rif_mainexportproducts = "",
                    rif_mainexportmarkets = "",

                    rif_plannedexportregionalmarkets = "",
                    rif_plannedexportcountry = "",
                    rif_receivinggvtexportassistance = false,
                    rif_exportassistancelocation = "",

                    rif_desiredservices = "908,510,000, 908,510,001",
                    rif_additionalcomments = "",
                    rif_currentchallenges = "",
                    rif_additionalareasofconcern = "",
                };
                try
                {
                    var queueService = new QueueService(config);
                    await queueService.SendMessage(account, "account-queue");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            static void SendMessageAsync()
            {
                // create a Service Bus client 
                ServiceBusClient client = new ServiceBusClient(connectionString);
                {
                    // create a sender for the queue 
                    ServiceBusSender sender = client.CreateSender(queueName);

                    // create a message that we can send
                    ServiceBusMessage message = new ServiceBusMessage("Hello world!");

                    // send the message
                    sender.SendMessageAsync(message);
                }
            }

            static async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();

                // complete the message. messages is deleted from the queue. 
                await args.CompleteMessageAsync(args.Message);
            }

            // handle any errors when receiving messages
            static Task ErrorHandler(ProcessErrorEventArgs args)
            {
                return Task.CompletedTask;
            }

            static Queue<ServiceBusMessage> CreateMessages()
            {
                // create a queue containing the messages and return it to the caller
                Queue<ServiceBusMessage> messages = new Queue<ServiceBusMessage>();
                messages.Enqueue(new ServiceBusMessage("First message in the batch"));
                messages.Enqueue(new ServiceBusMessage("Second message in the batch"));
                messages.Enqueue(new ServiceBusMessage("Third message in the batch"));
                return messages;
            }
        }
}
