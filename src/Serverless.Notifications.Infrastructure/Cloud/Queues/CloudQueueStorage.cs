using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Queues
{
    public class CloudQueueStorage : ICloudQueueStorage
    {
        private readonly string _connectionString;
       
        public CloudQueueStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc cref="SendMessageAsync"/>
        public async Task SendMessageAsync(string queueName, string message)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(queueName))
            {
                return;
            }

            QueueClient queueClient = CreateQueueClient(queueName);
            await queueClient.SendMessageAsync(message);
        }

        public async Task SendMessageToPoisonQueueAsync(string queueName, QueueMessage message)
        {
            QueueClient poisonQueueClient = await CreatePoisonQueueClientAsync(queueName);

            await poisonQueueClient.SendMessageAsync(message.Body);
            await DeleteMessagesAsync(queueName, message);
        }

        /// <inheritdoc cref="ReceiveMessagesAsync"/>
        public async Task<QueueMessage[]> ReceiveMessagesAsync(string queueName, int messageCount = 32)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                return new QueueMessage[0];
            }

            QueueClient queueClient = CreateQueueClient(queueName);
            return await queueClient.ReceiveMessagesAsync(messageCount);
        }

        /// <inheritdoc cref="PeekMessagesAsync"/>
        public async Task<PeekedMessage[]> PeekMessagesAsync(string queueName, int messageCount = 32)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                return new PeekedMessage[0];
            }

            QueueClient queueClient = CreateQueueClient(queueName);
            return await queueClient.PeekMessagesAsync(messageCount);
        }

        /// <inheritdoc cref="DeleteMessagesAsync"/>
        public async Task DeleteMessagesAsync(string queueName, QueueMessage message)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                return;
            }

            QueueClient queueClient = CreateQueueClient(queueName);
            await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }

        public async Task<int> GetApproximateMessagesCount(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                return 0;
            }

            QueueClient queueClient = CreateQueueClient(queueName);
            QueueProperties properties = await queueClient.GetPropertiesAsync();

            return properties.ApproximateMessagesCount;
        }

        public async Task CreateQueuesAsync(List<string> queueList)
        {
            foreach (string queue in queueList)
            {
                QueueClient queueClient = CreateQueueClient(queue);
                await queueClient.CreateIfNotExistsAsync();
            }
        }

        private QueueClient CreateQueueClient(string queueName)
        {
           QueueClient queueClient = new QueueClient(_connectionString, queueName, new QueueClientOptions
           {
                MessageEncoding = QueueMessageEncoding.Base64
           });

           return queueClient;
        }

        private async Task<QueueClient> CreatePoisonQueueClientAsync(string queueName)
        {
            QueueClient queueClient = new QueueClient(_connectionString, $"{queueName}-poison", new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            await queueClient.CreateIfNotExistsAsync();

            return queueClient;
        }
    }
}
