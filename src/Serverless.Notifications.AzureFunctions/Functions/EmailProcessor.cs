using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Serverless.Notifications.Application.Common.Interfaces;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.AzureFunctions.Functions;

public class EmailProcessor
{
    private const string EMAIL_QUEUE_NAME = "email";

    private readonly ISendGridService _sendGridService;

    public EmailProcessor(ISendGridService sendGridService)
    {
        _sendGridService = sendGridService;
    }

    [FunctionName("EmailProcessor")]
    public async Task RunAsync([QueueTrigger(EMAIL_QUEUE_NAME)] string queueMessage)
    {
        var email = JsonConvert.DeserializeObject<Email>(queueMessage);
        await _sendGridService.SendEmailAsync(email);
    }
}