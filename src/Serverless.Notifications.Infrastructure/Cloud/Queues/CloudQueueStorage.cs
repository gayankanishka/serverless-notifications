using Azure.Storage.Queues;
using Serverless.Notifications.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Serverless.Notifications.Infrastructure.Cloud.Queues
{
    public class CloudQueueStorage : INotificationPoolQueue, IEmailQueue, ISmsQueue, INotificationScheduleQueue
    {
        private readonly QueueClient _queueClient;

        public CloudQueueStorage(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
        }

        /// <inheritdoc cref="InsertMessageAsync"/>
        public async Task InsertMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            await _queueClient.CreateIfNotExistsAsync();

            if (await _queueClient.ExistsAsync())
            {
                await _queueClient.SendMessageAsync(message);
            }
        }
    }
}
