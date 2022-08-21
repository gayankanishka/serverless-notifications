using System.Threading.Tasks;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Constants;
using Serverless.Notifications.Domain.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Serverless.Notifications.Infrastructure.Services;

/// <inheritdoc />
public class TwilioSmsService : ITwilioSmsService
{
    #region Private Fields

    private readonly ITableConfiguration _tableConfiguration;

    #endregion

    #region Constructor

    /// <summary>
    ///     Constructs with DI.
    /// </summary>
    /// <param name="tableConfiguration"></param>
    public TwilioSmsService(ITableConfiguration tableConfiguration)
    {
        _tableConfiguration = tableConfiguration;
    }

    #endregion

    #region Twilio Operations

    /// <inheritdoc />
    public async Task<MessageResource> SendAsync(Sms sms)
    {
        var twilioAccountSid = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TWILIO_ACCOUNT_SID);
        var twilioAuthToken = await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TWILIO_AUTH_TOKEN);

        var twilioFromNumber = string.IsNullOrWhiteSpace(sms.FromNumber)
            ? await _tableConfiguration.GetSettingAsync(ConfigurationKeys.TWILIO_DEFAULT_FROM_NUMBER)
            : sms.FromNumber;

        TwilioClient.Init(twilioAccountSid, twilioAuthToken);

        return await MessageResource.CreateAsync(
            body: sms.MessageBody,
            from: new PhoneNumber(twilioFromNumber),
            to: new PhoneNumber(sms.ToNumber)
        );
    }

    #endregion
}