using Microsoft.Azure.WebJobs;
using Serverless.Notifications.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    public class ScheduleRunner
    {
        private readonly IScheduleProcessor _scheduleProcessor;
        
        public ScheduleRunner(IScheduleProcessor scheduleProcessor)
        {
            _scheduleProcessor = scheduleProcessor;
        }

        [FunctionName("ScheduleRunner")]
        public async Task Run([TimerTrigger("* * * * *")]TimerInfo myTimer)
        {
            await _scheduleProcessor.ProcessQueueAsync();
        }
    }
}
