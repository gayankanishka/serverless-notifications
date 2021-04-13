using Serverless.Notifications.Domain.Enums;
using System;

namespace Serverless.Notifications.Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public NotificationType NotificationType { get; set; }
        public DateTime TriggeredDateTime { get; set; } = DateTime.UtcNow;
        public string Body { get; set; } = string.Empty;
    }
}
