using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Serverless.Notifications.Infrastructure.Services
{
    /// <inheritdoc/>
    public class TwilioSmsService : ITwilioSmsService
    {
        #region Private Fields
        
        private readonly ITableConfiguration _tableConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs with DI.
        /// </summary>
        /// <param name="tableConfiguration"></param>
        public TwilioSmsService(ITableConfiguration tableConfiguration)
        {
            _tableConfiguration = tableConfiguration;
        }

        #endregion

        #region Twilio Operations

        /// <inheritdoc/>
        public async Task<MessageResource> SendAsync(Sms sms)
        {
            string twilioAccountSid = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TwilioAccountSid);
            string twilioAuthToken = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TwilioAuthToken);
            string twilioFromNumber = string.IsNullOrWhiteSpace(sms.FromNumber)
                ? await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TwilioDefaultFromNumber)
                : sms.FromNumber;
                
            TwilioClient.Init(twilioAccountSid, twilioAuthToken);

            return await MessageResource.CreateAsync(
                body: sms.MessageBody,
                from: new Twilio.Types.PhoneNumber(twilioFromNumber),
                to: new Twilio.Types.PhoneNumber(sms.ToNumber)
            );
        }

        #endregion
    }
}
