using System.Threading.Tasks;
using Serverless.Notifications.Domain.Models;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface INotificationPoolRouter
    {
        Task RouteNotification(Notification notification, bool scheduleEnabled = true);
    }
}
