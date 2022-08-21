using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Services;

/// <inheritdoc />
public class NotificationPoolRouter : INotificationPoolRouter
{
    #region Constructor

    /// <summary>
    ///     Constructs with DI.
    /// </summary>
    /// <param name="notificationScheduleQueue"></param>
    /// <param name="tableConfiguration"></param>
    public NotificationPoolRouter(ICloudQueueStorage notificationScheduleQueue, ITableConfiguration tableConfiguration)
    {
        _cloudQueueStorage = notificationScheduleQueue;
        _tableConfiguration = tableConfiguration;
    }

    #endregion

    #region Notification Router Operations

    /// <inheritdoc />
    public async Task RouteNotificationAsync(Notification notification, bool scheduleEnabled = true)
    {
        string queueName;

        if (notification.IsScheduled && scheduleEnabled)
        {
            var message = JsonConvert.SerializeObject(notification);
            queueName = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.SCHEDULE_QUEUE_NAME);

            await _cloudQueueStorage.SendMessageAsync(queueName, message);
            return;
        }

        var expectedQueue = notification.NotificationType.ToString().ToLower();
        queueName = await _tableConfiguration.GetSettingAsync(_ => _.Value.Contains(expectedQueue));

        if (string.IsNullOrWhiteSpace(queueName))
        {
            throw new Exception("Expected queue not found",
                new Exception("Please add the expected queue into configuration table"));
        }

        await _cloudQueueStorage.CreateQueueIfNotExistsAsync(queueName);
        await _cloudQueueStorage.SendMessageAsync(queueName, notification.Body);
    }

    #endregion

    #region Private Fields

    private readonly ICloudQueueStorage _cloudQueueStorage;
    private readonly ITableConfiguration _tableConfiguration;

    #endregion
}