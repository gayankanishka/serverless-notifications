using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;
using System.IO;
using System.Threading.Tasks;

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
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notifications")]
            HttpRequest request)
        {
            // TODO: Use [FromBody] attribute once issue is fixed
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            Notification notification = JsonConvert.DeserializeObject<Notification>(requestBody);

            await _notificationPool.SendMessageAsync(requestBody);

            return new AcceptedResult("notifications", notification.Id);
        }
    }
}

