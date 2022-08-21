using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Common.Interfaces;

public interface ISendGridService
{
    Task SendEmailAsync(Email email);
}