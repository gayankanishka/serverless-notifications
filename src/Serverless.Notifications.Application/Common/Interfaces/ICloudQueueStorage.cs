using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ICloudQueueStorage
    {
        /// <summary>
        /// Inserts a message into the queue
        /// </summary>
        /// <param name="message">Actual message</param>
        /// <returns></returns>
        Task InsertMessageAsync(string message);
    }
}
