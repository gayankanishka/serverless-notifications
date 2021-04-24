using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class NotificationController
    {
        private readonly ICloudQueueStorage _cloudQueueStorage;
        private readonly ITableConfiguration _tableConfiguration;

        public NotificationController(ICloudQueueStorage cloudQueueStorage, ITableConfiguration tableConfiguration)
        {
            _cloudQueueStorage = cloudQueueStorage;
            _tableConfiguration = tableConfiguration;
        }

        [FunctionName("PostNotifications")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notifications")]
            HttpRequest request)
        {
            // TODO: Use [FromBody] attribute once issue is fixed
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            Notification notification = JsonConvert.DeserializeObject<Notification>(requestBody);
            
            string queueName = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.NotificationPoolQueueName, "Queue");
            await _cloudQueueStorage.SendMessageAsync(queueName, requestBody);

            return new AcceptedResult("notifications", notification.Id);
        }
    }
}

