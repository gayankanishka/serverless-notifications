using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Services
{
    public class NotificationPoolRouter : INotificationPoolRouter
    {
        private readonly ICloudQueueStorage _cloudQueueStorage;
        private readonly ITableConfiguration _tableConfiguration;

        public NotificationPoolRouter(ICloudQueueStorage notificationScheduleQueue, ITableConfiguration tableConfiguration)
        {
            _cloudQueueStorage = notificationScheduleQueue;
            _tableConfiguration = tableConfiguration;
        }

        public async Task RouteNotification(Notification notification, bool scheduleEnabled = true)
        {
            string queueName;

            if (notification.IsScheduled && scheduleEnabled)
            {
                string message = JsonConvert.SerializeObject(notification);
                queueName = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.ScheduleQueueName);

                await _cloudQueueStorage.SendMessageAsync(queueName, message);
                return;
            }

            List<string> list = await _tableConfiguration.GetAllSettingsAsync("Queue");

            string expectedQueue = notification.NotificationType.ToString().ToLower();
            queueName = list.FirstOrDefault(_ => _.Contains(expectedQueue));

            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new Exception("Expected queue not found", 
                    new Exception("Please add the expected queue into configuration table"));
            }

            await _cloudQueueStorage.CreateQueueIfNotExistsAsync(queueName);
            await _cloudQueueStorage.SendMessageAsync(queueName, notification.Body);
        }
    }
}
