using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;

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
        public async Task Run([TimerTrigger("0 1 * * *")]TimerInfo myTimer)
        {
            await _scheduleProcessor.Process();
        }
    }
}
