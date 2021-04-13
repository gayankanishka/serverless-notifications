using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class NotificationController
    {
        private readonly INotificationPoolQueue _notificationPool;

        public NotificationController(INotificationPoolQueue notificationPool)
        {
            _notificationPool = notificationPool;
        }

        [FunctionName("PostNotifications")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "notifications")]
            [FromBody]Notification notification)
        {
            string message = JsonConvert.SerializeObject(notification);

            await _notificationPool.InsertMessageAsync(message);

            return new AcceptedResult("notifications", notification.Id);
        }
    }
}

