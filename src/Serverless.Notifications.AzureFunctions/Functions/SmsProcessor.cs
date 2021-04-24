using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class SmsProcessor
    {
        private readonly ITwilioSmsService _twilioSmsService;

        public SmsProcessor(ITwilioSmsService twilioSmsService)
        {
            _twilioSmsService = twilioSmsService;
        }

        [FunctionName("SmsProcessor")]
        public async Task Run([QueueTrigger("sms")] string queueMessage)
        {
            Sms sms = JsonConvert.DeserializeObject<Sms>(queueMessage);
            await _twilioSmsService.SendAsync(sms);
        }
    }
}
