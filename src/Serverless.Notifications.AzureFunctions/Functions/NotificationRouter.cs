using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class NotificationRouter
    {
        private const string NOTIFICATION_POOL_QUEUE_NAME = "notification-pool";

        private readonly INotificationPoolRouter _notificationPoolRouter;

        public NotificationRouter(INotificationPoolRouter notificationPoolRouter)
        {
            _notificationPoolRouter = notificationPoolRouter;
        }

        [FunctionName("NotificationRouter")]
        public async Task Run([QueueTrigger(NOTIFICATION_POOL_QUEUE_NAME)] string queueMessage)
        {
            Notification notification = JsonConvert.DeserializeObject<Notification>(queueMessage);
            await _notificationPoolRouter.RouteNotification(notification);
        }
    }
}
