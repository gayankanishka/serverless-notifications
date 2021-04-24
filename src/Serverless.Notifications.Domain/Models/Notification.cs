using System;
using Serverless.Notifications.Domain.Enums;

namespace Serverless.Notifications.Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public NotificationType NotificationType { get; set; }
        public DateTime TriggeredDateTime { get; set; } = DateTime.UtcNow;
        public bool IsScheduled { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
