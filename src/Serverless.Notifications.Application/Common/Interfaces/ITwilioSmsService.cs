using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;
using Twilio.Rest.Api.V2010.Account;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITwilioSmsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        Task<MessageResource> SendAsync(Sms sms);
    }
}
