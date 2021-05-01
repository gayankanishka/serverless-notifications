using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Serverless.Notifications.Application.Common.Interfaces;

namespace Serverless.Notifications.AzureFunctions.Functions
{
    /// <summary>
    /// Notification scheduler azure function.
    /// </summary>
    public class ScheduleRunner
    {
        #region Private fields

        private readonly IScheduleProcessor _scheduleProcessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs with DI.
        /// </summary>
        /// <param name="scheduleProcessor"></param>
        public ScheduleRunner(IScheduleProcessor scheduleProcessor)
        {
            _scheduleProcessor = scheduleProcessor;
        }

        #endregion

        #region Processor

        /// <summary>
        /// Run every time when the timer triggered to process queue messages.
        /// </summary>
        /// <param name="timer"></param>
        [FunctionName("ScheduleRunner")]
        public async Task Run([TimerTrigger("* * * * *")] TimerInfo timer)
        {
            await _scheduleProcessor.ProcessQueueAsync();
        }

        #endregion
    }
}
