using System;

namespace Serverless.Notifications.Domain.Models
{
    public class Sms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FromNumber { get; set; }
        public string ToNumber { get; set; } = string.Empty;
        public string MessageBody { get; set; } = string.Empty;
    }
}
