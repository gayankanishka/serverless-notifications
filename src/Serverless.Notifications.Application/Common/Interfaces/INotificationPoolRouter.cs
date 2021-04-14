using Serverless.Notifications.Domain.Models;
using System.Threading.Tasks;

namespace Serverless.Notifications.Application.Common.Interfaces
{
    public interface INotificationPoolRouter
    {
        Task RouteNotification(Notification notification, bool scheduleEnabled = true);
    }
}
