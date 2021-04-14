using Azure.Storage.Queues.Models;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ICloudQueueStorage
    {
        /// <summary>
        /// Inserts a message into the queue.
        /// </summary>
        /// <param name="message">Actual message</param>
        /// <returns></returns>
        Task SendMessageAsync(string message);

        /// <summary>
        /// Inserts a message into the queue.
        /// </summary>
        /// <param name="message">Actual message</param>
        /// <returns></returns>
        Task SendMessageToPoisonQueueAsync(QueueMessage message);

        /// <summary>
        /// Gets array of messages from the queue
        /// </summary>
        /// <param name="messageCount">Message batch size</param>
        /// <returns>Array of messages that retrieved from the queue</returns>
        Task<QueueMessage[]> ReceiveMessagesAsync(int messageCount = 32);

        /// <summary>
        /// Peeks at the next set of messages from the queue
        /// </summary>
        /// <param name="messageCount"></param>
        /// <returns></returns>
        Task<PeekedMessage[]> PeekMessagesAsync(int messageCount = 32);

        /// <summary>
        /// Deletes a message form the queue.
        /// </summary>
        /// <param name="message">Message that need to be deleted</param>
        /// <returns></returns>
        Task DeleteMessagesAsync(QueueMessage message);

        /// <summary>
        /// Returns the approximate message count on the queue.
        /// </summary>
        /// <returns></returns>
        Task<int> GetApproximateMessagesCount();
    }
}
