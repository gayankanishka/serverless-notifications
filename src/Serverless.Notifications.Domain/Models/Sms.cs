using System;

namespace Serverless.Notifications.Domain.Models
{
    public class Sms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string MessageBody { get; set; }
    }
}
