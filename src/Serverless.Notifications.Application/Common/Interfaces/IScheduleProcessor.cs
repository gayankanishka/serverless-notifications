using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces;

/// <summary>
///     Handles all the scheduled message operations.
/// </summary>
public interface IScheduleProcessor
{
    /// <summary>
    ///     Processes the scheduled queue message depending on the date.
    /// </summary>
    /// <returns></returns>
    Task ProcessQueueAsync();
}