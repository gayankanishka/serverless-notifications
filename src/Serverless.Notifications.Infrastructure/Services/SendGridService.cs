using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Infrastructure.Services;

public class SendGridService : ISendGridService
{
    private readonly ISendGridClient _sendGridClient;

    public SendGridService(ISendGridClient sendGridClient)
    {
        _sendGridClient = sendGridClient;
    }

    public async Task SendEmailAsync(Email email)
    {
        var sendGridMessage = BuildMessage(email);
        await _sendGridClient.SendEmailAsync(sendGridMessage);
    }

    private SendGridMessage BuildMessage(Email email)
    {
        var from = new EmailAddress(email.FromAddress, email.FromName);
        var to = new EmailAddress(email.ToAddress, email.ToName);

        return MailHelper.CreateSingleEmail(from, to, email.Subject, email.EmailTextContent, email.EmailHtmlContent);
    }
}