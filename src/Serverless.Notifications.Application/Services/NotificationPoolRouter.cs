using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Enums;
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

            if (notification.IsScheduled && scheduleEnabled)
            {
                string message = JsonConvert.SerializeObject(notification);
                string queueName = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.ScheduleQueueName);

                await _cloudQueueStorage.SendMessageAsync(queueName, message);
                return;
            }

            List<string> list = await _tableConfiguration.GetAllSettingsAsync("Queue");

            list.ForEach(async queue =>
            {
                switch (notification.NotificationType)
                {
                    case NotificationType.Sms:
                        await _cloudQueueStorage.SendMessageAsync(queue, notification.Body);
                        break;
                    case NotificationType.Email:
                        await _cloudQueueStorage.SendMessageAsync(queue, notification.Body);
                        break;
                }
            });
        }
    }
}
