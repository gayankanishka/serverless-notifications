using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;
using Twilio.Rest.Api.V2010.Account;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface ITwilioSmsService
    {
        Task<MessageResource> SendAsync(Sms sms);
    }
}
