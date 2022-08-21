using System.Threading.Tasks;
using Azure.Storage.Queues.Models;

namespace Serverless.Notifications.Application.Common.Interfaces;

/// <summary>
///     Handles all the cloud queue related operations.
/// </summary>
public interface ICloudQueueStorage
{
    /// <summary>
    ///     Inserts a message into the queue.
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <param name="message">Actual message</param>
    /// <returns></returns>
    Task SendMessageAsync(string queueName, string message);

    /// <summary>
    ///     Inserts a message into the poison queue.
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <param name="message">Actual message</param>
    /// <returns></returns>
    Task SendMessageToPoisonQueueAsync(string queueName, QueueMessage message);

    /// <summary>
    ///     Gets array of messages from the queue
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <param name="messageCount">Message batch size</param>
    /// <returns>Array of messages that retrieved from the queue</returns>
    Task<QueueMessage[]> ReceiveMessagesAsync(string queueName, int messageCount = 32);

    /// <summary>
    ///     Peeks at the next set of messages from the queue
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <param name="messageCount"></param>
    /// <returns></returns>
    Task<PeekedMessage[]> PeekMessagesAsync(string queueName, int messageCount = 32);

    /// <summary>
    ///     Deletes a message form the queue.
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <param name="message">Message that need to be deleted</param>
    /// <returns></returns>
    Task DeleteMessagesAsync(string queueName, QueueMessage message);

    /// <summary>
    ///     Returns the approximate message count on the queue.
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <returns></returns>
    Task<int> GetApproximateMessagesCountAsync(string queueName);

    /// <summary>
    ///     Creates a storage queue with provided queue name.
    /// </summary>
    /// <param name="queueName">Name of the storage queue.</param>
    /// <returns></returns>
    Task CreateQueueIfNotExistsAsync(string queueName);
}