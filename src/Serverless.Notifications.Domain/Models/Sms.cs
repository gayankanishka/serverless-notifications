using System;

namespace Serverless.Notifications.Domain.Models
{
    /// <summary>
    /// The SMS message contract.
    /// </summary>
    public class Sms
    {
        /// <summary>
        /// Get or set the unique ID of the SMS message.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// Get or set the Twilio from phone number.
        /// </summary>
        public string? FromNumber { get; set; }
        
        /// <summary>
        /// Get or set the sms recipient phone number.
        /// </summary>
        public string ToNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Get or set the SMS message body.
        /// </summary>
        public string MessageBody { get; set; } = string.Empty;
    }
}
