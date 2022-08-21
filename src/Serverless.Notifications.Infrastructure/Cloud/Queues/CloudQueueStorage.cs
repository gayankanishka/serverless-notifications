using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.Infrastructure.Cloud.Queues;

/// <inheritdoc />
public class CloudQueueStorage : ICloudQueueStorage
{
    #region Private Fields

    private readonly string _connectionString;

    #endregion

    #region Constructor

    /// <summary>
    ///     Constructs with a storage account connection string.
    /// </summary>
    /// <param name="connectionString">A cloud storage connection string.</param>
    public CloudQueueStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    #endregion

    #region Queue Operations

    /// <inheritdoc />
    public async Task SendMessageAsync(string queueName, string message)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        await queueClient.SendMessageAsync(message);
    }

    /// <inheritdoc />
    public async Task SendMessageToPoisonQueueAsync(string queueName, QueueMessage message)
    {
        var poisonQueueClient = await CreatePoisonQueueClientAsync(queueName);

        await poisonQueueClient.SendMessageAsync(message.Body);
        await DeleteMessagesAsync(queueName, message);
    }

    /// <inheritdoc />
    public async Task<QueueMessage[]> ReceiveMessagesAsync(string queueName, int messageCount = 32)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        return await queueClient.ReceiveMessagesAsync(messageCount);
    }

    /// <inheritdoc />
    public async Task<PeekedMessage[]> PeekMessagesAsync(string queueName, int messageCount = 32)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        return await queueClient.PeekMessagesAsync(messageCount);
    }

    /// <inheritdoc />
    public async Task DeleteMessagesAsync(string queueName, QueueMessage message)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
    }

    /// <inheritdoc />
    public async Task<int> GetApproximateMessagesCountAsync(string queueName)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        QueueProperties properties = await queueClient.GetPropertiesAsync();

        return properties.ApproximateMessagesCount;
    }

    /// <inheritdoc />
    public async Task CreateQueueIfNotExistsAsync(string queueName)
    {
        ThrowIfNotSpecified(queueName);

        var queueClient = CreateQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync();
    }

    #endregion

    #region Helper Methods

    private QueueClient CreateQueueClient(string queueName)
    {
        var queueClient = new QueueClient(_connectionString, queueName, new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        });

        return queueClient;
    }

    private async Task<QueueClient> CreatePoisonQueueClientAsync(string queueName)
    {
        var queueClient = new QueueClient(_connectionString, $"{queueName}-poison", new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        });

        await queueClient.CreateIfNotExistsAsync();

        return queueClient;
    }

    private void ThrowIfNotSpecified(string queueName)
    {
        if (string.IsNullOrWhiteSpace(queueName))
        {
            throw new Exception("Queue name not specified");
        }
    }

    #endregion
}