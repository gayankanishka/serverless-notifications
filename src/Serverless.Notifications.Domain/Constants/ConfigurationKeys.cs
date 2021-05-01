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
        public const string NotificationPoolQueueName = "NotificationPool";
        
        /// <summary>
        /// The email processor queue name config.
        /// </summary>
        public const string EmailQueueName = "EmailQueue";
        
        /// <summary>
        /// The sms processor queue name config.
        /// </summary>
        public const string SmsQueueName = "SmsQueue";
        
        /// <summary>
        /// The schedule processor queue name config.
        /// </summary>
        public const string ScheduleQueueName = "ScheduleQueue";

        /// <summary>
        /// The Twilio provided Account SID.
        /// </summary>
        public const string TwilioAccountSid = "Twilio.AccountSid";
        
        /// <summary>
        /// The Twilio provided authentication token.
        /// </summary>
        public const string TwilioAuthToken = "Twilio.AuthToken";
        
        /// <summary>
        /// The default Twilio from phone number.
        /// </summary>
        public const string TwilioDefaultFromNumber = "Twilio.DefaultFromNumber";
    }
}
