using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    /// <summary>
    /// Handles all the <see cref="TwilioClient"/> operations.
    /// </summary>
    public interface ITwilioSmsService
    {
        /// <summary>
        /// Sends a <see cref="Sms"/> message through <see cref="TwilioClient"/>.
        /// </summary>
        /// <param name="sms">The SMS payload.</param>
        /// <returns>Twilio message response.</returns>
        Task<MessageResource> SendAsync(Sms sms);
    }
}
