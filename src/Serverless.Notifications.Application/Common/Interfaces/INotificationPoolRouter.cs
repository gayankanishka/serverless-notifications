using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Common.Interfaces;

/// <summary>
///     Handles notification routing with storage queues.
/// </summary>
public interface INotificationPoolRouter
{
    /// <summary>
    ///     Routes the notification into required queue according to notification type.
    /// </summary>
    /// <param name="notification">The notification.</param>
    /// <param name="scheduleEnabled">Enables the scheduled notification routing.</param>
    /// <returns></returns>
    Task RouteNotificationAsync(Notification notification, bool scheduleEnabled = true);
}