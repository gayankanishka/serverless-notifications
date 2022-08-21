using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions;

/// <summary>
///     Notification router azure function.
/// </summary>
public class NotificationRouter
{
    #region Constructor

    /// <summary>
    ///     Constructs with DI.
    /// </summary>
    /// <param name="notificationPoolRouter"></param>
    public NotificationRouter(INotificationPoolRouter notificationPoolRouter)
    {
        _notificationPoolRouter = notificationPoolRouter;
    }

    #endregion

    #region Processor

    /// <summary>
    ///     Routs notification into relevant queue.
    /// </summary>
    /// <param name="queueMessage"></param>
    [FunctionName("NotificationRouter")]
    public async Task Run([QueueTrigger(NOTIFICATION_POOL_QUEUE_NAME)] string queueMessage)
    {
        var notification = JsonConvert.DeserializeObject<Notification>(queueMessage);
        await _notificationPoolRouter.RouteNotificationAsync(notification);
    }

    #endregion

    #region Private Fields

    private const string NOTIFICATION_POOL_QUEUE_NAME = "notification-pool";

    private readonly INotificationPoolRouter _notificationPoolRouter;

    #endregion
}