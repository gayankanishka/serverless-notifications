namespace Serverless.Notifications.Domain.Constants
{
    /// <summary>
    /// Holds all the table storage configuration RowKeys.
    /// </summary>
    public static class ConfigurationKeys
    {
        /// <summary>
        /// The notification pool queue name config.
        /// </summary>
        public const string NOTIFICATION_POOL_QUEUE_NAME = "Queue.NotificationPool";
        
        /// <summary>
        /// The email processor queue name config.
        /// </summary>
        public const string EMAIL_QUEUE_NAME = "Queue.EmailQueue";
        
        /// <summary>
        /// The sms processor queue name config.
        /// </summary>
        public const string SMS_QUEUE_NAME = "Queue.SmsQueue";
        
        /// <summary>
        /// The schedule processor queue name config.
        /// </summary>
        public const string SCHEDULE_QUEUE_NAME = "Queue.ScheduleQueue";

        /// <summary>
        /// The Twilio provided Account SID.
        /// </summary>
        public const string TWILIO_ACCOUNT_SID = "Twilio.AccountSid";
        
        /// <summary>
        /// The Twilio provided authentication token.
        /// </summary>
        public const string TWILIO_AUTH_TOKEN = "Twilio.AuthToken";
        
        /// <summary>
        /// The default Twilio from phone number.
        /// </summary>
        public const string TWILIO_DEFAULT_FROM_NUMBER = "Twilio.DefaultFromNumber";
    }
}
