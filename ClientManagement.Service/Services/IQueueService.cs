using System.Threading.Tasks;

namespace ClientManagement.Service.Services
{
    public interface IQueueService
    {
        Task SendMessage<T>(T serviceMessageBus, string queueName);
    }
}