using Microsoft.Extensions.Configuration;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Serverless.Notifications.Infrastructure.Services
{
    public class TwilioSmsService : ITwilioSmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioFromNumber;

        public TwilioSmsService(IConfiguration configuration)
        {
            _accountSid = configuration.GetSection("TWILIO_ACCOUNT_SID").Value;
            _authToken = configuration.GetSection("TWILIO_AUTH_TOKEN").Value;
            _twilioFromNumber = configuration.GetSection("TWILIO_FROM_NUMBER").Value;
        }

        public async Task<MessageResource> SendAsync(Sms sms)
        {
            TwilioClient.Init(_accountSid, _authToken);

            return await MessageResource.CreateAsync(
                body: sms.MessageBody,
                from: new Twilio.Types.PhoneNumber(string.IsNullOrWhiteSpace(sms.FromNumber) ? _twilioFromNumber : sms.FromNumber),
                to: new Twilio.Types.PhoneNumber(sms.ToNumber)
            );
        }
    }
}
