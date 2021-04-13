using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Serverless.Notifications.Domain.Models;
using System.Threading.Tasks;

namespace Serverless.Notifications.Api
{
    public static class NotificationController
    {
        [FunctionName("notifications")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            ILogger log, [FromBody]Notification notification)
        {
            log.LogInformation("Notification Id",notification.Id);
            log.LogInformation("Notification Body",notification.Body);



            return new AcceptedResult("notifications", notification.Id);
        }
    }
}

