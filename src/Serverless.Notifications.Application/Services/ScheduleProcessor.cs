using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Services
{
    public class ScheduleProcessor : IScheduleProcessor
    {
        private const string SCHEDULE_QUEUE_NAME = "scheduled-notifications";

        private readonly ICloudQueueStorage _cloudQueueStorage;
        private readonly INotificationPoolRouter _notificationPoolRouter;

        public ScheduleProcessor(ICloudQueueStorage cloudQueueStorage, INotificationPoolRouter notificationPoolRouter)
        {
            _cloudQueueStorage = cloudQueueStorage;
            _notificationPoolRouter = notificationPoolRouter;
        }

        public async Task ProcessQueueAsync()
        {
            while (true)
            {
                List<Task> tasks = new List<Task>();
                QueueMessage[] messages = await _cloudQueueStorage.ReceiveMessagesAsync(SCHEDULE_QUEUE_NAME);

                foreach (QueueMessage message in messages)
                {
                    tasks.Add(ProcessMessageAsync(message));
                }

                await Task.WhenAll(tasks);

                PeekedMessage[] peekedMessages = await _cloudQueueStorage.PeekMessagesAsync(SCHEDULE_QUEUE_NAME);

                if (await _cloudQueueStorage.GetApproximateMessagesCount(SCHEDULE_QUEUE_NAME) == 0 || peekedMessages.Length == 0)
                {
                    break;
                }
            }
        }

        private async Task ProcessMessageAsync(QueueMessage message)
        {
            if (message.DequeueCount >= 5)
            {
                await _cloudQueueStorage.SendMessageToPoisonQueueAsync(SCHEDULE_QUEUE_NAME, message);
                return;
            }

            Notification notification = JsonConvert.DeserializeObject<Notification>(message.MessageText);

            if (notification.ScheduledDateTime.Date != DateTime.UtcNow.Date)
            {
                return;
            }

            await _notificationPoolRouter.RouteNotification(notification, false);

            await _cloudQueueStorage.DeleteMessagesAsync(SCHEDULE_QUEUE_NAME, message);
        }
    }
}
