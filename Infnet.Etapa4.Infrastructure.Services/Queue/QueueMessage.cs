using Azure.Storage.Queues;
using Infnet.Etapa4.Domain.Model.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Infrastructure.Services.Queue
{
    public class QueueMessage : IQueueMessage
    {
        private readonly QueueServiceClient _queueServiceClient;
        private const string _queueName = "filamail";

        public QueueMessage(string storageAccount)
        {
            _queueServiceClient = new QueueServiceClient(storageAccount);
        }

        public async Task SendAsync(string messageText)
        {
            var queueClient = _queueServiceClient.GetQueueClient(_queueName);

            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(messageText);
        }
    }
}
