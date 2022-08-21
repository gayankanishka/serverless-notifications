using System;
using Serverless.Notifications.Domain.Enums;

namespace Serverless.Notifications.Domain.Models;

/// <summary>
///     The Notification contract of the application.
/// </summary>
public class Notification
{
    /// <summary>
    ///     Get or set the unique ID of the notification.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     Get or set the notification type.
    /// </summary>
    public NotificationType NotificationType { get; set; }

    /// <summary>
    ///     Get or set the notification triggered date time.
    /// </summary>
    public DateTime TriggeredDateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     Get or set the scheduled notification flag.
    /// </summary>
    public bool IsScheduled { get; set; }

    /// <summary>
    ///     Get or set the notification scheduled date time.
    /// </summary>
    public DateTime ScheduledDateTime { get; set; }

    /// <summary>
    ///     Get or set the notification body with the serialized notification type object.
    /// </summary>
    public string Body { get; set; } = string.Empty;
}