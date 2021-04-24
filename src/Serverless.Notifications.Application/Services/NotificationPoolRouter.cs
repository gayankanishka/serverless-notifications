using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Enums;
using Serverless.Notifications.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Services
{
    public class NotificationPoolRouter : INotificationPoolRouter
    {
        private readonly INotificationScheduleQueue _notificationScheduleQueue;
        private readonly ISmsQueue _smsQueue;
        private readonly IEmailQueue _emailQueue;

        public NotificationPoolRouter(INotificationScheduleQueue notificationScheduleQueue,
            ISmsQueue smsQueue, IEmailQueue emailQueue)
        {
            _notificationScheduleQueue = notificationScheduleQueue;
            _smsQueue = smsQueue;
            _emailQueue = emailQueue;
        }

        public async Task RouteNotification(Notification notification, bool scheduleEnabled = true)
        {
            if (notification.IsScheduled && scheduleEnabled)
            {
                string message = JsonConvert.SerializeObject(notification);

                await _notificationScheduleQueue.SendMessageAsync(message);
                return;
            }

            switch (notification.NotificationType)
            {
                case NotificationType.Sms:
                    await _smsQueue.SendMessageAsync(notification.Body);
                    break;
                case NotificationType.Email:
                    await _emailQueue.SendMessageAsync(notification.Body);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
