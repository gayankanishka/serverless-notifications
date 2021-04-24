using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Queues
{
    public class CloudQueueStorage : INotificationPoolQueue, IEmailQueue, ISmsQueue, INotificationScheduleQueue
    {
        private readonly QueueClient _queueClient;
        private readonly QueueClient _poisonQueueClient;

        public CloudQueueStorage(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            _poisonQueueClient = new QueueClient(connectionString, $"{queueName}-poison", new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        }

        /// <inheritdoc cref="SendMessageAsync"/>
        public async Task SendMessageAsync(string message)
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

        public async Task SendMessageToPoisonQueueAsync(QueueMessage message)
        {
            await _poisonQueueClient.CreateIfNotExistsAsync();

            if (await _poisonQueueClient.ExistsAsync())
            {
                await _poisonQueueClient.SendMessageAsync(message.Body);
                await DeleteMessagesAsync(message);
            }
        }

        /// <inheritdoc cref="ReceiveMessagesAsync"/>
        public async Task<QueueMessage[]> ReceiveMessagesAsync(int messageCount = 32)
        {
            if (await _queueClient.ExistsAsync())
            {
                return await _queueClient.ReceiveMessagesAsync(messageCount);
            }

            return null;
        }

        /// <inheritdoc cref="PeekMessagesAsync"/>
        public async Task<PeekedMessage[]> PeekMessagesAsync(int messageCount = 32)
        {
            if (await _queueClient.ExistsAsync())
            {
                return await _queueClient.PeekMessagesAsync(messageCount);
            }

            return null;
        }

        /// <inheritdoc cref="DeleteMessagesAsync"/>
        public async Task DeleteMessagesAsync(QueueMessage message)
        {
            if (await _queueClient.ExistsAsync())
            {
                await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);

            }
        }

        public async Task<int> GetApproximateMessagesCount()
        {
            if (!await _queueClient.ExistsAsync())
            {
                return 0;
            }

            QueueProperties properties = await _queueClient.GetPropertiesAsync();

            return properties.ApproximateMessagesCount;
        }
    }
}
