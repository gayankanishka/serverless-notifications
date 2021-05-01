using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    /// <summary>
    /// SMS processor azure function.
    /// </summary>
    public class SmsProcessor
    {
        #region Private Fields

        private const string SMS_QUEUE_NAME = "sms";

        private readonly ITwilioSmsService _twilioSmsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs with DI.
        /// </summary>
        /// <param name="twilioSmsService"></param>
        public SmsProcessor(ITwilioSmsService twilioSmsService)
        {
            _twilioSmsService = twilioSmsService;
        }

        #endregion
        
        #region Processor

        /// <summary>
        /// Process and send sms messages in the queue.
        /// </summary>
        /// <param name="queueMessage"></param>
        [FunctionName("SmsProcessor")]
        public async Task Run([QueueTrigger(SMS_QUEUE_NAME)] string queueMessage)
        {
            Sms sms = JsonConvert.DeserializeObject<Sms>(queueMessage);
            await _twilioSmsService.SendAsync(sms);
        }

        #endregion
    }
}
