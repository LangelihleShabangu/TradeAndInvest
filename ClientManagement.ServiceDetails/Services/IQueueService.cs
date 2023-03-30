using System.Threading.Tasks;

namespace ClientManagement.ServiceDetails.Services
{
    public interface IQueueService
    {
        void SendMessage<T>(T serviceMessageBus, string queueName);
    }
}