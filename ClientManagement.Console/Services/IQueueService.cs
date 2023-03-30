using System.Threading.Tasks;

namespace ClientManagement.Console.Services
{
    public interface IQueueService
    {
        Task SendMessage<T>(T serviceMessageBus, string queueName);
    }
}