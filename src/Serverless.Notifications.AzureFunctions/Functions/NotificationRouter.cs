using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class NotificationRouter
    {
        private readonly INotificationPoolRouter _notificationPoolRouter;

        public NotificationRouter(INotificationPoolRouter notificationPoolRouter)
        {
            _notificationPoolRouter = notificationPoolRouter;
        }

        [FunctionName("NotificationRouter")]
        public async Task Run([QueueTrigger("notification-pool")] string queueMessage)
        {
            Notification notification = JsonConvert.DeserializeObject<Notification>(queueMessage);
            await _notificationPoolRouter.RouteNotification(notification);
        }
    }
}
