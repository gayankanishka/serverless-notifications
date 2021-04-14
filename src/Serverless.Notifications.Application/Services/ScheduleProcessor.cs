using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Services
{
    public class ScheduleProcessor : IScheduleProcessor
    {
        private readonly INotificationScheduleQueue _notificationScheduleQueue;
        private readonly INotificationPoolRouter _notificationPoolRouter;

        public ScheduleProcessor(INotificationScheduleQueue notificationScheduleQueue, INotificationPoolRouter notificationPoolRouter)
        {
            _notificationScheduleQueue = notificationScheduleQueue;
            _notificationPoolRouter = notificationPoolRouter;
        }

        public async Task ProcessQueueAsync()
        {
            while (true)
            {
                List<Task> tasks = new List<Task>();
                QueueMessage[] messages = await _notificationScheduleQueue.ReceiveMessagesAsync();

                foreach (QueueMessage message in messages)
                {
                    tasks.Add(ProcessMessageAsync(message));
                }

                await Task.WhenAll(tasks);

                PeekedMessage[] peekedMessages = await _notificationScheduleQueue.PeekMessagesAsync();

                if (await _notificationScheduleQueue.GetApproximateMessagesCount() == 0 || peekedMessages.Length == 0)
                {
                    break;
                }
            }
        }

        private async Task ProcessMessageAsync(QueueMessage message)
        {
            if (message.DequeueCount >= 10)
            {
                await _notificationScheduleQueue.SendMessageToPoisonQueueAsync(message);
                return;
            }

            Notification notification = JsonConvert.DeserializeObject<Notification>(message.MessageText);

            if (notification.ScheduledDateTime.Date != DateTime.UtcNow.Date)
            {
                return;
            }

            await _notificationPoolRouter.RouteNotification(notification, false);

            await _notificationScheduleQueue.DeleteMessagesAsync(message);
        }
    }
}
